using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions.Repositories
{
    public interface IDeliveryRepository
    {
        Task AddAsync(Delivery entity, CancellationToken cancellationToken);
        Task UpdateAsync(Delivery entity, CancellationToken cancellationToken);
        Task<Delivery> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Delivery> GetByOrderAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
