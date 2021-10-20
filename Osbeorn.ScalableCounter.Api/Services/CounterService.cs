using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Cassandra;
using Microsoft.Extensions.Configuration;
using Cassandra.Mapping;
using Osbeorn.ScalableCounter.Db;
using Osbeorn.ScalableCounter.Domain;

namespace Osbeorn.ScalableCounter.Api.Services
{
    public class CounterService
    {
        private readonly IConfiguration _configuration;
        private readonly CassandraDbContext _dbContext;

        public CounterService(IConfiguration configuration, CassandraDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Counter>> GetAll()
        {
            var session = _dbContext.GetSession();
            var mapper = new Mapper(session);
            
            await mapper.ExecuteAsync("UPDATE counters SET count = count + 1 WHERE id = 1");
            
            return await mapper.FetchAsync<Counter>("SELECT * FROM counters");
        }
    }
}