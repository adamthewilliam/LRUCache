using Finbourne.Components.LRUCache.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Finbourne.Components.LRUCache.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class LRUCacheSampleController : ControllerBase
{
    private readonly ILRUCache<string, int> _lruCache;
    
    public LRUCacheSampleController(ILRUCache<string, int> lruCache)
    {
        _lruCache = lruCache;
    }

    [HttpGet(Name = "GetCachedValue")]
    public ActionResult Get(string key)
    {
        var result = _lruCache.Get(key);

        if (result == default)
        {
            return BadRequest($"The key '{key}' does not exist.");
        }
        
        return Ok($"The value for the key '{key}' is {result}");
    }
    
    [HttpPost(Name = "PostCachedValue")]
    public ActionResult Post(string key, int value)
    {
        _lruCache.Add(key, value);
        return Ok($"Successfully added key '{key}' with value '{value}'");
    }
}