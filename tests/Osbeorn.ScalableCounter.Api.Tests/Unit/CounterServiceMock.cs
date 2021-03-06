using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Osbeorn.ScalableCounter.Api.Services;
using Osbeorn.ScalableCounter.Domain;

namespace Osbeorn.ScalableCounter.Api.Tests.Unit
{
    public class CounterServiceMock : ICounterService
    {
        public Task<IEnumerable<Counter>> IncrementAndGetAllAsync()
        {
            var counters = new List<Counter>
            {
                new()
                {
                    Id = "192.168.1.132",
                    Count = 5
                },
                new()
                {
                    Id = "192.168.1.144",
                    Count = 3
                }
            };

            return Task.FromResult(counters.AsEnumerable());
        }
    }
}