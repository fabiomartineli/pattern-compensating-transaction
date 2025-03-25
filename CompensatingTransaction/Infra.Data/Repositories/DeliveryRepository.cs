using Domain.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Threading;
using System;
using Domain.Abstractions.Repositories;

namespace Infra.Data.Repositories
{
    public class DeliveryRepository : BaseRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(IBaseRepositoryInjector injector) : base(injector, "Delivery")
        {

        }

        public async Task<Delivery> GetByOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.OrderId == orderId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
