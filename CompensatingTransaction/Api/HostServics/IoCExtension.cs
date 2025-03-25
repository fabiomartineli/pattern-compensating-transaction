using Api.HostServics.Deliveries;
using Api.HostServics.Orders;
using Api.HostServics.Payments;
using Api.HostServics.Stocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.HostServics
{
    public static class IoCExtension
    {
        public static IServiceCollection AddMessageBusConsumers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<OrderCancelConsumer>();
            services.AddHostedService<OrderCreateConsumer>();
            services.AddHostedService<OrderConfirmDeliveryConsumer>();
            services.AddHostedService<OrderConfirmPaymentConsumer>();
            services.AddHostedService<OrderConfirmStockConsumer>();
            services.AddHostedService<OrderStartPaymentConsumer>();

            services.AddHostedService<PaymentRequestConsumer>();
            services.AddHostedService<PaymentCancelConsumer>();
            services.AddHostedService<PaymentConfirmConsumer>();
            services.AddHostedService<PaymentFailConsumer>();

            services.AddHostedService<StockCreateConsumer>();
            services.AddHostedService<StockCancelConsumer>();

            services.AddHostedService<DeliveryCreateConsumer>();
            services.AddHostedService<DeliveryConfirmConsumer>();
            services.AddHostedService<DeliveryFailConsumer>();

            return services;
        }
    }
}
