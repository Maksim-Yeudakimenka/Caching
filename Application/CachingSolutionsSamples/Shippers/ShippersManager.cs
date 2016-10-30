using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Shippers
{
    public class ShippersManager
    {
        private readonly IShippersCache _cache;

        public ShippersManager(IShippersCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Shipper> GetShippers()
        {
            Console.WriteLine("Get Shippers");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var shippers = _cache.Get(user);

            if (shippers == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    shippers = dbContext.Shippers.ToList();
                    _cache.Set(user, shippers);
                }
            }

            return shippers;
        }
    }
}