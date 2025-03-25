using Domain.Abstractions.Repositories;
using Domain.Entities;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IBaseRepositoryInjector injector) : base(injector, "Order")
        {

        }

        public Task<Order> GetByNumberAsync(string number, CancellationToken cancellationToken)
        {
            return _collection.Find(x => x.Number == number).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
