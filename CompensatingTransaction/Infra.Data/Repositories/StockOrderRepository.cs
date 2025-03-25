using Domain.Abstractions.Repositories;
using Domain.Entities;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class StockOrderRepository : BaseRepository<StockOrder>, IStockOrderRepository
    {
        public StockOrderRepository(IBaseRepositoryInjector injector) : base(injector, "Stock")
        {

        }

        public async Task<StockOrder> GetByOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.OrderId == orderId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
