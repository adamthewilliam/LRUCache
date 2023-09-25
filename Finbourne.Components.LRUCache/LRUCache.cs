using Finbourne.Components.LRUCache.Contracts;

namespace Finbourne.Components.LRUCache;

internal class LRUCache<TKey, TValue> : ILRUCache<TKey, TValue> where TKey : notnull 
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<LRUCacheItem<TKey, TValue>>> _cache;
    private readonly LinkedList<LRUCacheItem<TKey, TValue>> _doubleLinkedList;
 
    internal LRUCache(int capacity)
    {
        _capacity = capacity;
        _cache = new Dictionary<TKey, LinkedListNode<LRUCacheItem<TKey, TValue>>>(_capacity);
        _doubleLinkedList = new LinkedList<LRUCacheItem<TKey, TValue>>();
    }
    
    public TValue Get(TKey key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        
        if (_cache.TryGetValue(key, out var node))
        {
            TValue value = node.Value.Value!;

            if (value == null)
            {
                throw new InvalidOperationException(nameof(value));
            }

            lock (_doubleLinkedList)
            {
                _doubleLinkedList.Remove(node);
                _doubleLinkedList.AddFirst(node);
            }
            
            return value;
        }
        else
        {
            return default!;
        }
    }

    public void Add(TKey key, TValue value)
    {
        if (_cache.Count >= _capacity)
        {
            RemoveLRUItem();
        }

        LRUCacheItem<TKey, TValue> lruCacheItem = new LRUCacheItem<TKey, TValue>
        {
            Key = key,
            Value = value
        };
        LinkedListNode<LRUCacheItem<TKey, TValue>> node = new LinkedListNode<LRUCacheItem<TKey, TValue>>(lruCacheItem);

        lock (_doubleLinkedList)
        {
            _doubleLinkedList.AddFirst(node);
        }
        
        _cache.Add(key, node);
    }

    public int GetCount()
    {
        return _cache.Count;
    }

    private void RemoveLRUItem()
    {
        lock (_cache)
        {
            lock (_doubleLinkedList)
            {
                LinkedListNode<LRUCacheItem<TKey, TValue>> lastNode = _doubleLinkedList.Last!;

                if (lastNode.Value.Key == null)
                {
                    throw new NullReferenceException(nameof(lastNode.Value.Key));
                }
                
                _cache.Remove(lastNode.Value.Key);
                _doubleLinkedList.RemoveLast();
            }
        }
    }
}