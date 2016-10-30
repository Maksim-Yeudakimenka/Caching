using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Caching;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Customers
{
    public class CustomersMemoryCache : ICustomersCache
    {
        private const string Prefix = "Cache_Customers";
        private readonly ObjectCache _cache = MemoryCache.Default;

        public IEnumerable<Customer> Get(string forUser)
        {
            return (IEnumerable<Customer>) _cache.Get(Prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Customer> customers)
        {
            var policy = new CacheItemPolicy();

            var sqlMonitor = new SqlChangeMonitor(
                new SqlDependency(
                    new SqlCommand("SELECT * FROM dbo.Customers")));

            policy.ChangeMonitors.Add(sqlMonitor);

            _cache.Set(Prefix + forUser, customers, policy);
        }
    }
}