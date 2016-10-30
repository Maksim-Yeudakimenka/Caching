using System.Runtime.Caching;

namespace Fibonacci
{
    public class FibonacciMemoryCache : IFibonacciCache
    {
        private readonly ObjectCache _cache = MemoryCache.Default;

        public long? Get(string key)
        {
            return (long?) _cache.Get(key);
        }

        public void Set(string key, long value)
        {
            _cache.Set(key, value, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
