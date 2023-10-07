namespace Finbourne.Components.LRUCache.Contracts;

public interface ILRUCache<TKey, TValue>
{
    TValue Get(TKey key);
    
    void Add(TKey key, TValue value);
    
    event EventHandler<TKey> ItemRemoved; 

    int GetCount();
}