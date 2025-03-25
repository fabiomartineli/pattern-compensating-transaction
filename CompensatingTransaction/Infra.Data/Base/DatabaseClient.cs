using Infra.Data.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infra.Data.Base
{
    public class DatabaseClient : IDatabaseClient
    {
        private readonly MongoClient _client;

        public DatabaseClient(IOptions<DatabaseContextSettings> options)
        {
            _client = new MongoClient(options.Value.ConnectionString);

            RegisterSerializers();
        }

        public IMongoClient Client => _client;

        private static void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
    }
}
