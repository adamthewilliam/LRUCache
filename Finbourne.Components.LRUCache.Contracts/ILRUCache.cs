namespace Finbourne.Components.LRUCache.Contracts;

public interface ILRUCache<in TKey, TValue>
{
    TValue Get(TKey key);
    
    void Add(TKey key, TValue value);

    int GetCount();
}