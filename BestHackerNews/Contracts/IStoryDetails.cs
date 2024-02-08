using BestHackerNews.Model;

namespace BestHackerNews.Contracts
{
    public interface IStoryDetails
    {
        public Task<IEnumerable<StoryDetail>> GetStoryDetails(IEnumerable<int> storyIds);
    }
}
