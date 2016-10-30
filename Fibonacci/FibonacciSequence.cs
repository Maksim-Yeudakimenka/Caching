using System;
using System.Collections;
using System.Collections.Generic;

namespace Fibonacci
{
    public class FibonacciSequence : IEnumerable<long>
    {
        private readonly int _count;
        private readonly IFibonacciCache _cache;

        public FibonacciSequence(int count, IFibonacciCache cache)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count should be greater than zero.");
            }

            _count = count;
            _cache = cache;
        }

        public IEnumerator<long> GetEnumerator()
        {
            var a = 0L;
            var b = 1L;
            var c = 1L;

            for (var i = 1; i <= _count; i++)
            {
                yield return c;

                var nextKey = (i + 1).ToString();
                var nextValue = _cache.Get(nextKey);

                if (nextValue != null)
                {
                    Console.WriteLine("Cache hit");

                    c = nextValue.Value;
                }
                else
                {
                    Console.WriteLine("Cache miss");

                    c = a + b;
                    _cache.Set(nextKey, c);
                }

                a = b;
                b = c;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}