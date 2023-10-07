using Finbourne.Components.LRUCache.Contracts;
using FluentAssertions;

namespace Finbourne.Components.LRUCache.UnitTests;

public class LRUCacheTests
{
    private const string LRUKey = "Potato";
    
    [Fact]
    public void When_Capacity_Is_Reached_LRU_Should_Be_Removed_From_Cache()
    {
        // Arrange
        var lruCache = LRUCacheFactory.Create<string, int>(5);

        // Act
        lruCache.Add("Lotus root", 9);

        // Assert
        lruCache.Get(LRUKey).Should().Be(default);
        lruCache.Get("Lotus root");
        lruCache.GetCount().Should().Be(5);
    }

    [Fact]
    public void When_LRU_Is_Removed_From_Cache_An_Event_Should_Be_Raised()
    {
        // Arrange
        var lruCache = LRUCacheFactory.Create<string, int>(5);
        SetupCacheToTestLRU(lruCache);
        
        var wasEventHandlerCalled = false;
        lruCache.ItemRemoved += (sender, key) => wasEventHandlerCalled = true;

        // Act
        lruCache.Add("Lotus root", 9);
        
        // Assert
        Assert.True(wasEventHandlerCalled);
    }
    
    private void SetupCacheToTestLRU(ILRUCache<string, int> lruCache)
    {

        const string sweetPotato = "Sweet potato";
        const string tomato = "Tomato";

        lruCache.Add(LRUKey, 5);
        lruCache.Add(sweetPotato, 7);
        lruCache.Add("Broccoli", 2);
        lruCache.Add(tomato, 4);
        lruCache.Add("Mushroom", 6);

        lruCache.Get("Broccoli");
        lruCache.Get(tomato);
        lruCache.Get(sweetPotato);
    }
}