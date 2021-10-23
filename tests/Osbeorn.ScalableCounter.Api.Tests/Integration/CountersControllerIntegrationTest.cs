using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Osbeorn.ScalableCounter.Api.Controllers;
using Osbeorn.ScalableCounter.Api.Services;
using Osbeorn.ScalableCounter.Db;
using Xunit;

namespace Osbeorn.ScalableCounter.Api.Tests.Integration
{
    public class CountersControllerIntegrationTest : IDisposable
    {
        private readonly ILogger<CountersController> _logger = NullLogger<CountersController>.Instance;
        private readonly CountersController _controller;
        private readonly ICassandraDbContext _cassandraDbContext;
        
        public CountersControllerIntegrationTest()
        {
            _cassandraDbContext = new CassandraDbContextMock();
            _controller = new CountersController(_logger, new CounterService(_logger, _cassandraDbContext));
        }

        [Fact]
        public void IncrementAndGetAllReturnsCounters()
        {
            var resultTask = _controller.IncrementAndGetAll();

            var result = resultTask.Result;
            
            Assert.NotNull(result);

            var resultList = result.ToList();
            
            Assert.NotEmpty(resultList);
            Assert.Single(resultList);

            var firstResult = resultList.First();
            
            Assert.NotNull(firstResult);
            Assert.Equal("127.0.0.1", firstResult.Id);
            Assert.Equal(1, firstResult.Count);
        }

        public void Dispose()
        {
            if (_cassandraDbContext != null)
            {
                _cassandraDbContext.Dispose();
            }
        }
    }
}