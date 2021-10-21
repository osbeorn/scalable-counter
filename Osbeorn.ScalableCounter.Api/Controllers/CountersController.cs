using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Osbeorn.ScalableCounter.Api.Services;
using Osbeorn.ScalableCounter.Domain;

namespace Osbeorn.ScalableCounter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountersController : ControllerBase
    {
        private readonly ILogger<CountersController> _logger;
        private readonly ICounterService _counterService;

        public CountersController(ILogger<CountersController> logger, ICounterService counterService)
        {
            _logger = logger;
            _counterService = counterService;
        }

        [HttpGet]
        public async Task<IEnumerable<Counter>> GetAll()
        {
            _logger.LogInformation("Received 'GetAll' request");

            return await _counterService.GetAll();
        }
    }
}