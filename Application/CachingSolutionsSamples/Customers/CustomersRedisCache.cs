using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using NorthwindLibrary;
using StackExchange.Redis;

namespace CachingSolutionsSamples.Customers
{
    public class CustomersRedisCache : ICustomersCache
    {
        private const string Prefix = "Cache_Customers";
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(IEnumerable<Customer>));

        public CustomersRedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<Customer> Get(string forUser)
        {
            var db = _redisConnection.GetDatabase();
            byte[] s = db.StringGet(Prefix + forUser);
            if (s == null)
            {
                return null;
            }

            return (IEnumerable<Customer>) _serializer.ReadObject(new MemoryStream(s));

        }

        public void Set(string forUser, IEnumerable<Customer> customers)
        {
            var db = _redisConnection.GetDatabase();
            var key = Prefix + forUser;

            if (customers == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var expirationTime = TimeSpan.FromMinutes(1);

                var stream = new MemoryStream();
                _serializer.WriteObject(stream, customers);
                db.StringSet(key, stream.ToArray(), expirationTime);
            }
        }
    }
}