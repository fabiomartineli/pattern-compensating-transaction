using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Amazon.SQS;
using Amazon.SQS.Model;
using Domain.Abstractions.Saga;
using Infra.MessageBus.Client;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.MessageBus.Consumer
{
    public class MessageBusConsumer : IMessageBusConsumer
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _serviceProvider;
        private readonly MessageBusClientSettings _settings;
        private readonly ILogger<MessageBusConsumer> _logger;

        public MessageBusConsumer(IMessageBusClient messageBusClient,
            IMediator mediator,
            IServiceScopeFactory serviceProvider,
            ILogger<MessageBusConsumer> logger,
            IOptions<MessageBusClientSettings> settings)
        {
            _messageBusClient = messageBusClient;
            _mediator = mediator;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task ExecuteAsync<TContent>(MessageBusConsumerRequest request, CancellationToken cancellationToken) where TContent : IRequest
        {
            await Task.Factory.StartNew(async () =>
            {
                var url = $"{_settings.BaseQueueUrl}{request.Destination}";
                var maxRetryQueue = await QueueMaxQueueDeliveryCountAsync(url, cancellationToken);
                var receiveRequest = CreateReceiveRequest(url);

                while (true)
                {
                    var result = await _messageBusClient.QueueClient.ReceiveMessageAsync(receiveRequest, cancellationToken);

                    foreach (var message in result.Messages)
                    {
                        TContent content = default;
                        message.Attributes.TryGetValue(MessageSystemAttributeName.ApproximateReceiveCount, out var approximateReceiveCount);
                        using var scope = _serviceProvider.CreateAsyncScope();

                        try
                        {
                            if (message.Body.Contains("Notification"))
                            {
                                var messageBody = JsonSerializer.Deserialize<MessageBusConsumerNotificationMessage>(message.Body);
                                content = JsonSerializer.Deserialize<TContent>(messageBody.Message);
                            }
                            else
                            {
                                content = JsonSerializer.Deserialize<TContent>(message.Body);
                            }

                            await scope.ServiceProvider.GetRequiredService<IMediator>().Send(content, cancellationToken);
                            await _messageBusClient.QueueClient.DeleteMessageAsync(request.Destination, message.ReceiptHandle, cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);

                            if (Convert.ToInt32(approximateReceiveCount) >= maxRetryQueue)
                            {
                                var compensatingTransaction = scope.ServiceProvider.GetRequiredService<ISaga<TContent>>();
                                await compensatingTransaction.CompensateTransactionAsync(content, default);
                            }
                        }

                        await scope.DisposeAsync();
                    }
                }
            });
        }

        #region Private Methods

        private async Task<int> QueueMaxQueueDeliveryCountAsync(string url, CancellationToken cancellationToken)
        {
            var queue = await _messageBusClient.QueueClient.GetQueueAttributesAsync(new GetQueueAttributesRequest
            {
                QueueUrl = url,
                AttributeNames = ["RedrivePolicy"]
            }, cancellationToken);

            var attributeSpan = queue.Attributes["RedrivePolicy"].AsSpan();
            var maxReceiveCountStartIndex = attributeSpan.IndexOf("\"maxReceiveCount\":");
            var maxReceiveCountSpan = attributeSpan[maxReceiveCountStartIndex..];
            var maxReceiveCountEndIndex = maxReceiveCountSpan.IndexOf(':');
            var maxRetryQueue = maxReceiveCountSpan.Slice(maxReceiveCountEndIndex + 1, 1).Trim();

            return Convert.ToInt32(maxRetryQueue.ToString());
        }

        private static ReceiveMessageRequest CreateReceiveRequest(string url)
        {
            return new ReceiveMessageRequest
            {
                QueueUrl = url,
                MessageSystemAttributeNames = [MessageSystemAttributeName.ApproximateReceiveCount]
            };
        }

        #endregion
    }
}
