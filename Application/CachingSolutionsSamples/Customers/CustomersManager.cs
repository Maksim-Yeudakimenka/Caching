using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Customers
{
    public class CustomersManager
    {
        private readonly ICustomersCache _cache;

        public CustomersManager(ICustomersCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            Console.WriteLine("Get Customers");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var customers = _cache.Get(user);

            if (customers == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    customers = dbContext.Customers.ToList();
                    _cache.Set(user, customers);
                }
            }

            return customers;
        }
    }
}