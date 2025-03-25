using MongoDB.Driver;

namespace Infra.Data.Base
{
    public interface IDatabaseContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
    }
}
