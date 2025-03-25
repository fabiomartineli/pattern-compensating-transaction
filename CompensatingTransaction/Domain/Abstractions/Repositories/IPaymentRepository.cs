using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment entity, CancellationToken cancellationToken);
        Task UpdateAsync(Payment entity, CancellationToken cancellationToken);
        Task<Payment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Payment> GetByOrderAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
