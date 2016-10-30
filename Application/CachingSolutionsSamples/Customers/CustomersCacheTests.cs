using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CachingSolutionsSamples.Customers
{
    [TestClass]
    public class CustomersCacheTests
    {
        [TestMethod]
        public void CustomersMemoryCacheTest()
        {
            var customerManager = new CustomersManager(new CustomersMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(customerManager.GetCustomers().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void CustomersRedisCacheTest()
        {
            var customerManager = new CustomersManager(new CustomersRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(customerManager.GetCustomers().Count());
                Thread.Sleep(100);
            }
        }
    }
}