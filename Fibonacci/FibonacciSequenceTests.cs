using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fibonacci
{
    [TestClass]
    public class FibonacciSequenceTests
    {
        [TestMethod]
        public void FibonacciMemoryCacheTest()
        {
            var cache = new FibonacciMemoryCache();

            var first = new FibonacciSequence(5, cache);
            var second = new FibonacciSequence(10, cache);

            foreach (var number in first)
            {
                Console.Write($"{number} ");
            }

            foreach (var number in second)
            {
                Console.Write($"{number} ");
            }
        }

        [TestMethod]
        public void FibonacciRedisCacheTest()
        {
            var cache = new FibonacciRedisCache("localhost");

            var sequence = new FibonacciSequence(10, cache);

            foreach (var number in sequence)
            {
                Console.Write($"{number} ");
            }
        }
    }
}