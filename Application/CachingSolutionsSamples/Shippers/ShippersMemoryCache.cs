using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Caching;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Shippers
{
    public class ShippersMemoryCache : IShippersCache
    {
        private const string Prefix = "Cache_Shippers";
        private readonly ObjectCache _cache = MemoryCache.Default;

        public IEnumerable<Shipper> Get(string forUser)
        {
            return (IEnumerable<Shipper>) _cache.Get(Prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Shipper> shippers)
        {
            var policy = new CacheItemPolicy();

            var sqlMonitor = new SqlChangeMonitor(
                new SqlDependency(
                    new SqlCommand("SELECT * FROM dbo.Shippers")));

            policy.ChangeMonitors.Add(sqlMonitor);

            _cache.Set(Prefix + forUser, shippers, policy);
        }
    }
}