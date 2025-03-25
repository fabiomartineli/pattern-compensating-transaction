using Domain.Abstractions.Repositories;
using Infra.Data.Base;
using Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Data
{
    public static class IoCExtension
    {
        public static IServiceCollection AddDatabaseIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDatabaseClient, DatabaseClient>();
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseRepositoryInjector, BaseRepositoryInjector>();

            services.AddScoped<IStockOrderRepository, StockOrderRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
