using StackExchange.Redis;

namespace Fibonacci
{
    public class FibonacciRedisCache : IFibonacciCache
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public FibonacciRedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public long? Get(string key)
        {
            var db = _redisConnection.GetDatabase();

            return (long?) db.StringGet(key);
        }

        public void Set(string key, long value)
        {
            var db = _redisConnection.GetDatabase();

            db.StringSet(key, value);
        }
    }
}
