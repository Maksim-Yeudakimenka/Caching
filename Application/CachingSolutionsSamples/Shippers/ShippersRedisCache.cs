using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using NorthwindLibrary;
using StackExchange.Redis;

namespace CachingSolutionsSamples.Shippers
{
    public class ShippersRedisCache : IShippersCache
    {
        private const string Prefix = "Cache_Shippers";
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(IEnumerable<Shipper>));

        public ShippersRedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<Shipper> Get(string forUser)
        {
            var db = _redisConnection.GetDatabase();
            byte[] s = db.StringGet(Prefix + forUser);
            if (s == null)
            {
                return null;
            }

            return (IEnumerable<Shipper>) _serializer.ReadObject(new MemoryStream(s));

        }

        public void Set(string forUser, IEnumerable<Shipper> shippers)
        {
            var db = _redisConnection.GetDatabase();
            var key = Prefix + forUser;

            if (shippers == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var expirationTime = TimeSpan.FromMinutes(1);

                var stream = new MemoryStream();
                _serializer.WriteObject(stream, shippers);
                db.StringSet(key, stream.ToArray(), expirationTime);
            }
        }
    }
}