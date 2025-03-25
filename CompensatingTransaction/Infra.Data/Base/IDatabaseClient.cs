using MongoDB.Driver;

namespace Infra.Data.Base
{
    public interface IDatabaseClient
    {
        IMongoClient Client { get; }
    }
}
