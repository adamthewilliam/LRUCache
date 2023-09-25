using Finbourne.Components.LRUCache.Contracts;

namespace Finbourne.Components.LRUCache;

public static class LRUCacheFactory
{
    public static ILRUCache<TKey, TValue> Create<TKey, TValue>(int capacity) where TKey : notnull
    {
        return new LRUCache<TKey, TValue>(capacity);
    }
}