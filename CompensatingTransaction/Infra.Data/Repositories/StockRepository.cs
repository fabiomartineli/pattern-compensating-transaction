using Domain.Abstractions.Repositories;
using Domain.Entities;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(IBaseRepositoryInjector injector) : base(injector, "Stock")
        {

        }

        public async Task<Stock> GetByProductAsync(Guid productId, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.ProductId == productId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
