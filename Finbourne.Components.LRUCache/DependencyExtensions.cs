using Microsoft.Extensions.DependencyInjection;

namespace Finbourne.Components.LRUCache;

public static class DependencyExtensions
{
    public static void AddLRUCache<TKey, TValue>(this IServiceCollection serviceCollection, int capacity) where TKey : notnull
    {
        var lruCache = LRUCacheFactory.Create<TKey, TValue>(capacity);
        serviceCollection.AddSingleton(lruCache);
    }
}