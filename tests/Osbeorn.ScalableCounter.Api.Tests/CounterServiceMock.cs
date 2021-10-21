using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Osbeorn.ScalableCounter.Api.Services;
using Osbeorn.ScalableCounter.Domain;

namespace Osbeorn.ScalableCounter.Api.Tests
{
    public class CounterServiceMock : ICounterService
    {
        public Task<IEnumerable<Counter>> GetAll()
        {
            var counters = new List<Counter>()
            {
                new()
                {
                    Id = "1",
                    Count = 5
                },
                new()
                {
                    Id = "2",
                    Count = 3
                }
            };

            return Task.FromResult(counters.AsEnumerable());
        }
    }
}