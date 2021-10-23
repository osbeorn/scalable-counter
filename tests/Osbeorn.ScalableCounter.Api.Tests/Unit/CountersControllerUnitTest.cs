using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Osbeorn.ScalableCounter.Api.Controllers;
using Osbeorn.ScalableCounter.Api.Services;
using Xunit;

namespace Osbeorn.ScalableCounter.Api.Tests.Unit
{
    public class CountersControllerUnitTest
    {
        private readonly ILogger<CountersController> _logger = NullLogger<CountersController>.Instance;
        private readonly ICounterService _counterService = new CounterServiceMock();
        private readonly CountersController _controller;
        
        public CountersControllerUnitTest()
        {
            _controller = new CountersController(_logger, _counterService);
        }

        [Fact]
        public void IncrementAndGetAllReturnsCounters()
        {
            var resultTask = _controller.IncrementAndGetAll();

            var result = resultTask.Result;
            
            Assert.NotNull(result);

            var resultList = result.ToList();
            
            Assert.NotEmpty(resultList);
            Assert.Equal(2, resultList.Count);
        }
    }
}