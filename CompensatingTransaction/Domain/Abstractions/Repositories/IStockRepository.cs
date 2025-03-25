using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions.Repositories
{
    public interface IStockRepository
    {
        Task AddAsync(Stock entity, CancellationToken cancellationToken);
        Task UpdateAsync(Stock entity, CancellationToken cancellationToken);
        Task<Stock> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Stock> GetByProductAsync(Guid productId, CancellationToken cancellationToken);
    }
}
