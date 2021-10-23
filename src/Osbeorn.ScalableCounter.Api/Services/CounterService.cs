using System;
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
        private readonly ICassandraDbContext _dbContext;

        public CounterService(ILogger<CountersController> logger, ICassandraDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Counter>> IncrementAndGetAllAsync()
        {
            var session = await _dbContext.GetSession();
            var mapper = new Mapper(session);

            var realIp = "127.0.0.1"; // fallback
            try
            {
                var host = await Dns.GetHostEntryAsync(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        realIp = ip.ToString();
                    }
                }
            }
            catch (Exception)
            {
                // should only occur in tests but log anyway
                _logger.LogWarning("Failed to retrieve host IP address. Using fallback IP 127.0.0.1.");
            }

            _logger.LogInformation($"Increasing counter for id={realIp}");
            
            await mapper.ExecuteAsync($"UPDATE counters SET count = count + 1 WHERE id = '{realIp}'");
            
            _logger.LogInformation("Retrieving all counters");
            
            return await mapper.FetchAsync<Counter>("SELECT * FROM counters");
        }
    }
}