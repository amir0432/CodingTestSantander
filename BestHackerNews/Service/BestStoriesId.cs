using BestHackerNews.Common;
using BestHackerNews.Contracts;

namespace BestHackerNews.Service
{
    public class BestStoriesId : IBestStoriesId
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public BestStoriesId(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient();
            _config = config;
        }
        public async Task<IEnumerable<int>> GetBestStoriesIds()
        {
            var uri = _config.GetSection(Constants.bestStoriesUri).Value;
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var bestStoryIds = await response.Content.ReadFromJsonAsync<IEnumerable<int>>();
            return bestStoryIds;
        }
    }
}
