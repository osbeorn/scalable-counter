using System.IO;
using System.Threading.Tasks;
using Cassandra;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Osbeorn.ScalableCounter.Db;
using Serilog;

namespace Osbeorn.ScalableCounter.Api.Tests
{
    public class CassandraDbContextMock : ICassandraDbContext
    {
        private readonly string CASSANDRA_USER = "cassandra";
        private readonly string CASSANDRA_PASSWORD = "cassandra";
        private readonly string CASSANDRA_KEYSPACE = "test";
        
        private IDockerContainer _testcontainer;
        private Cluster _cluster;
        private ISession _session;

        public async Task<ISession> GetSession()
        {
            await SetupTestContainerCassandra();

            _cluster =
                Cluster.Builder()
                    .AddContactPoint("localhost")
                    .WithCredentials(CASSANDRA_USER, CASSANDRA_PASSWORD)
                    .WithDefaultKeyspace(CASSANDRA_KEYSPACE)
                    .Build();

            _session = await _cluster.ConnectAsync();

            return _session;
        }

        private async Task SetupTestContainerCassandra()
        {
            var outputConsumer = new RedirectOutputToStream(new MemoryStream(), new MemoryStream());
            
            var testcontainerBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("docker.io/bitnami/cassandra:4.0")
                .WithPortBinding(9042)
                .WithCleanUp(true)
                .WithMount("../../../../../cassandra", "/docker-entrypoint-initdb.d")
                .WithOutputConsumer(outputConsumer)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(outputConsumer.Stdout, "Initializing test.counters"));

            _testcontainer = testcontainerBuilder.Build();

            await _testcontainer.StartAsync();
        }
        
        public void Dispose()
        {
            _testcontainer?.StopAsync();
            _cluster?.Dispose();
            _session?.Dispose();
        }
    }
}