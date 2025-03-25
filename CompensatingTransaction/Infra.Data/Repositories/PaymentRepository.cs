using Domain.Abstractions.Repositories;
using Domain.Entities;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class PaymentRepository :  BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IBaseRepositoryInjector injector) : base(injector, "Payment")
        {

        }

        public async Task<Payment> GetByOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.OrderId == orderId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
