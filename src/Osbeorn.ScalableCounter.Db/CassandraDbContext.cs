using System.Threading.Tasks;
using Cassandra;
using Microsoft.Extensions.Configuration;

namespace Osbeorn.ScalableCounter.Db
{
    public class CassandraDbContext : ICassandraDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly Cluster _cluster;
        private readonly ISession _session;
        
        public CassandraDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            
            var cassandraConfig = _configuration.GetSection("AppConfiguration:Cassandra");
            
            _cluster =
                Cluster.Builder()
                    .AddContactPoint(cassandraConfig.GetValue<string>("ContactPoint"))
                    .WithCredentials(cassandraConfig.GetValue<string>("Username"), cassandraConfig.GetValue<string>("Password"))
                    .WithDefaultKeyspace(cassandraConfig.GetValue<string>("DefaultKeyspace"))
                    .Build();

            _session = _cluster.Connect();
        }

        public Task<ISession> GetSession()
        {
            return Task.FromResult(_session);
        }

        public void Dispose()
        {
            _cluster?.Dispose();
            _session?.Dispose();
        }
    }
}