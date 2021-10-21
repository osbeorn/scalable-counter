using System.Collections.Generic;
using System.Threading.Tasks;
using Osbeorn.ScalableCounter.Domain;

namespace Osbeorn.ScalableCounter.Api.Services
{
    public interface ICounterService
    {
        public Task<IEnumerable<Counter>> GetAllAsync();
    }
}