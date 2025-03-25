using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions.Repositories
{
    public interface IStockOrderRepository
    {
        Task AddAsync(StockOrder entity, CancellationToken cancellationToken);
        Task UpdateAsync(StockOrder entity, CancellationToken cancellationToken);
        Task<StockOrder> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<StockOrder> GetByOrderAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
