using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Cassandra.Mapping;
using Microsoft.Extensions.Logging;
using Osbeorn.ScalableCounter.Api.Controllers;
using Osbeorn.ScalableCounter.Db;
using Osbeorn.ScalableCounter.Domain;

namespace Osbeorn.ScalableCounter.Api.Services
{
    public class CounterService : ICounterService
    {
        private readonly ILogger<CountersController> _logger;
        private readonly IConfiguration _configuration;
        private readonly CassandraDbContext _dbContext;

        public CounterService(ILogger<CountersController> logger, IConfiguration configuration, CassandraDbContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Counter>> GetAllAsync()
        {
            var session = _dbContext.GetSession();
            var mapper = new Mapper(session);

            var realIp = "127.0.0.1"; // fallback
            var host = await Dns.GetHostEntryAsync(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    realIp = ip.ToString();
                }
            }
            
            _logger.LogInformation($"Increasing counter for id={realIp}");
            
            await mapper.ExecuteAsync($"UPDATE counters SET count = count + 1 WHERE id = '{realIp}'");
            
            _logger.LogInformation("Retrieving all counters");
            
            return await mapper.FetchAsync<Counter>("SELECT * FROM counters");
        }
    }
}