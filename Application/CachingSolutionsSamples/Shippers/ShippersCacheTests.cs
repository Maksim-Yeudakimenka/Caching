using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CachingSolutionsSamples.Shippers
{
    [TestClass]
    public class ShippersCacheTests
    {
        [TestMethod]
        public void ShippersMemoryCacheTest()
        {
            var shipperManager = new ShippersManager(new ShippersMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(shipperManager.GetShippers().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void ShippersRedisCacheTest()
        {
            var shipperManager = new ShippersManager(new ShippersRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(shipperManager.GetShippers().Count());
                Thread.Sleep(100);
            }
        }
    }
}