namespace Finbourne.Components.LRUCache;

public class LRUCacheItem<TKey, TValue>
{
    public TKey? Key { get; set; }
    public TValue? Value { get; set; }
}