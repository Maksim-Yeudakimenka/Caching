using System.Collections.Generic;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Shippers
{
    public interface IShippersCache
    {
        IEnumerable<Shipper> Get(string forUser);
        void Set(string forUser, IEnumerable<Shipper> shippers);
    }
}