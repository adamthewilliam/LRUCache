namespace Finbourne.Components.LRUCache;

public class LRUCacheItem<TKey, TValue>
{
    public TKey Key { get; set; } = default!;
    public TValue Value { get; set; } = default!;
}