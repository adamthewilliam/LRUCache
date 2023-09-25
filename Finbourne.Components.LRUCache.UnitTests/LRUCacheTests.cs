using FluentAssertions;
using System.Threading;

namespace Finbourne.Components.LRUCache.UnitTests;

public class LRUCacheTests
{
    [Fact]
    public void When_Capacity_Is_Reached_LRU_Should_Be_Removed_From_Cache()
    {
        // Arrange
        var lruCache = LRUCacheFactory.Create<string, int>(5);
        const string lruKey = "Potato";
        const string sweetPotato = "Sweet potato";
        const string tomato = "Tomato";
        // Act
        lruCache.Add(lruKey, 5);
        lruCache.Add(sweetPotato, 7);
        lruCache.Add("Broccoli", 2);
        lruCache.Add(tomato, 4);
        lruCache.Add("Mushroom", 6);
        
        lruCache.Get("Broccoli");
        lruCache.Get(tomato);
        lruCache.Get(sweetPotato);
        
        lruCache.Add("Lotus root", 9);
        
        // Assert
        lruCache.Get(lruKey).Should().Be(default);
        lruCache.Get("Lotus root");
        lruCache.GetCount().Should().Be(5);
    }
}