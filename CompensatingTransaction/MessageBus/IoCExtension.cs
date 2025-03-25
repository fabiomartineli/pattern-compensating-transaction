using Infra.MessageBus.Client;
using Infra.MessageBus.Consumer;
using Infra.MessageBus.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.MessageBus
{
    public static class IoCExtension
    {
        public static IServiceCollection AddMessageBusIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddSingleton<IMessageBusConsumer, MessageBusConsumer>();
            services.AddSingleton<IMessageBusPublisher, MessageBusPublisher>();

            return services;
        }
    }
}
