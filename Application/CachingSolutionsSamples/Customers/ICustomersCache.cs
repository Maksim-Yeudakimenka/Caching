using System.Collections.Generic;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Customers
{
    public interface ICustomersCache
    {
        IEnumerable<Customer> Get(string forUser);
        void Set(string forUser, IEnumerable<Customer> customers);
    }
}