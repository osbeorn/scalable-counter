using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Osbeorn.ScalableCounter.Api.Controllers;
using Osbeorn.ScalableCounter.Api.Services;
using Xunit;

namespace Osbeorn.ScalableCounter.Api.Tests
{
    public class CountersControllerTest
    {
        private readonly ILogger<CountersController> _logger = NullLogger<CountersController>.Instance;
        private readonly ICounterService _counterService = new CounterServiceMock();
        private readonly CountersController _controller;
        
        public CountersControllerTest()
        {
            _controller = new CountersController(_logger, _counterService);
        }

        [Fact]
        public void GetAllReturnsCounters()
        {
            var resultTask = _controller.GetAll();

            var result = resultTask.Result;
            
            Assert.NotNull(result);

            var resultList = result.ToList();
            
            Assert.NotEmpty(resultList);
            Assert.Equal(2, resultList.Count);
        }
    }
}