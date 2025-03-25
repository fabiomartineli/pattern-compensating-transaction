using Infra.Data.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Data.Base
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IDatabaseClient _client;
        private readonly DatabaseContextSettings _settings;

        public DatabaseContext(IDatabaseClient client, IOptions<DatabaseContextSettings> options)
        {
            _client = client;
            _settings = options.Value;
        }

        public IMongoClient Client => _client.Client;
        public IMongoDatabase Database => Client.GetDatabase(_settings.DataBase);
    }
}
