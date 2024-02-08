using BestHackerNews.Common;
using BestHackerNews.Contracts;
using BestHackerNews.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BestHackerNews.Controllers
{
    [ApiController]
    public class BestStoriesController : Controller
    {
        private readonly IBestStoriesId _bestStoriesId;
        private readonly IStoryDetails _storyDetails;
        private readonly ILogger<BestStoriesController> _logger;
        private readonly IMemoryCache _cache;

        public BestStoriesController(IBestStoriesId bestStoriesId, IStoryDetails storyDetails, ILogger<BestStoriesController>
            logger, IMemoryCache cache)
        {
            _bestStoriesId = bestStoriesId;
            _storyDetails = storyDetails;
            _logger = logger;
            _cache = cache;
        }

        [HttpGet("api/best-stories/{n}")]
        public async Task<ActionResult<IEnumerable<Story>>> GetBestStories(int n)
        {
            if (!_cache.TryGetValue("BestStories", out IEnumerable<Story> cachedStories))
            {
                try
                {
                    var bestStoryIds = await _bestStoriesId.GetBestStoriesIds();
                    var bestStories = await _storyDetails.GetStoryDetails(bestStoryIds.Take(n));
                    var bestStoriesResult = FillStoryFromStoryDetails(bestStories);

                    // Cache the fetched data for 1 minute to service large number of requests without risking overloading of the Hacker News API
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                    _cache.Set("BestStories", bestStoriesResult, cacheEntryOptions);

                    cachedStories = bestStoriesResult;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    return StatusCode(500, "An error occurred while processing the request.");
                }
            }
            
            return cachedStories.ToList();
        }

        private List<Story> FillStoryFromStoryDetails(IEnumerable<StoryDetail> storyDetails)
        {
            List<Story> result = new List<Story>();
            foreach(var story in storyDetails)
            {
                result.Add(new Story
                {
                    Title = story.Title,
                    Uri = story.Url,
                    PostedBy = story.By,
                    Time = Utils.UnixTimeStampToDateTime(story.Time),
                    Score = story.Score,
                    CommentCount = story.Descendants
                });
            }
            return result;
        }
        
    }
}
