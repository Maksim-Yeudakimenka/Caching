namespace Fibonacci
{
    public interface IFibonacciCache
    {
        long? Get(string key);

        void Set(string key, long value);
    }
}
