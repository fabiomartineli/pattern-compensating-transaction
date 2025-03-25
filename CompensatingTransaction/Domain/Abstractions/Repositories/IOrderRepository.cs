using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order entity, CancellationToken cancellationToken);
        Task UpdateAsync(Order entity, CancellationToken cancellationToken);
        Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Order> GetByNumberAsync(string number, CancellationToken cancellationToken);
    }
}
