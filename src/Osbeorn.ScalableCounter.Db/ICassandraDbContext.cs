using System;
using System.Threading.Tasks;
using Cassandra;

namespace Osbeorn.ScalableCounter.Db
{
    public interface ICassandraDbContext : IDisposable
    {
        public Task<ISession> GetSession();
    }
}