using Application.Commands.Deliveries.Confirm;
using Application.Commands.Deliveries.Create;
using Application.Commands.Deliveries.Fail;
using Application.Commands.Orders.Cancel;
using Application.Commands.Orders.ConfirmDelivery;
using Application.Commands.Orders.ConfirmPayment;
using Application.Commands.Orders.ConfirmStock;
using Application.Commands.Orders.Create;
using Application.Commands.Orders.PaymentProcessing;
using Application.Commands.Payments.Cancel;
using Application.Commands.Payments.Confirm;
using Application.Commands.Payments.Fail;
using Application.Commands.Payments.Request;
using Application.Commands.Stocks.Cancel;
using Application.Commands.Stocks.Create;
using Domain.Abstractions.Saga;
using Domain.Commands.Deliveries.Confirm;
using Domain.Commands.Deliveries.Create;
using Domain.Commands.Deliveries.Fail;
using Domain.Commands.Orders.Cancel;
using Domain.Commands.Orders.ConfirmDelivery;
using Domain.Commands.Orders.ConfirmPayment;
using Domain.Commands.Orders.ConfirmStock;
using Domain.Commands.Orders.Create;
using Domain.Commands.Orders.StartDelivery;
using Domain.Commands.Orders.StartPayment;
using Domain.Commands.Payments.Cancel;
using Domain.Commands.Payments.Confirm;
using Domain.Commands.Payments.Fail;
using Domain.Commands.Payments.Request;
using Domain.Commands.Stocks.Cancel;
using Domain.Commands.Stocks.Create;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class IoCExtension
    {
        public static IServiceCollection AddApplicationIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(IoCExtension).Assembly));

            services.AddScoped<ISaga<CreateDeliveryCommand>, CreateDeliveryCommandHandler>();
            services.AddScoped<ISaga<FailDeliveryCommand>, FailDeliveryCommandHandler>();
            services.AddScoped<ISaga<ConfirmDeliveryCommand>, ConfirmDeliveryCommandHandler>();

            services.AddScoped<ISaga<CreateStockCommand>, CreateStockCommandHandler>();
            services.AddScoped<ISaga<CancelStockCommand>, CancelStockCommandHandler>();

            services.AddScoped<ISaga<RequestPaymentCommand>, RequestPaymentCommandHandler>();
            services.AddScoped<ISaga<CancelPaymentCommand>, CancelPaymentCommandHandler>();
            services.AddScoped<ISaga<FailPaymentCommand>, FailPaymentCommandHandler>();
            services.AddScoped<ISaga<ConfirmPaymentCommand>, ConfirmPaymentCommandHandler>();

            services.AddScoped<ISaga<CreateOrderCommand>, CreateOrderCommandHandler>();
            services.AddScoped<ISaga<CancelOrderCommand>, CancelOrderCommandHandler>();
            services.AddScoped<ISaga<StartPaymentOrderCommand>, StartPaymentOrderCommandHandler>();
            services.AddScoped<ISaga<ConfirmDeliveryOrderCommand>, ConfirmDeliveryOrderCommandHandler>();
            services.AddScoped<ISaga<ConfirmPaymentOrderCommand>, ConfirmPaymentOrderCommandHandler>();
            services.AddScoped<ISaga<ConfirmStockOrderCommand>, ConfirmStockOrderCommandHandler>();


            return services;
        }
    }
}
