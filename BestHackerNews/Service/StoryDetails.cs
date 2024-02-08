using BestHackerNews.Common;
using BestHackerNews.Contracts;
using BestHackerNews.Model;

namespace BestHackerNews.Service
{
    public class StoryDetails : IStoryDetails
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public StoryDetails(IHttpClientFactory httpClientFactory, IConfiguration config) 
        {
            _httpClient = httpClientFactory.CreateClient();
            _config = config;
        }
        public async Task<IEnumerable<StoryDetail>> GetStoryDetails(IEnumerable<int> storyIds)
        {
            var uri = _config.GetSection(Constants.storyDetailsUri).Value;
            var tasks = storyIds.Select(async id =>
            {
                var response = await _httpClient.GetAsync(uri + id + Constants.json);
                response.EnsureSuccessStatusCode();
                var story = await response.Content.ReadFromJsonAsync<StoryDetail>();
                return story;
            });

            return await Task.WhenAll(tasks);
        }
    }
}
