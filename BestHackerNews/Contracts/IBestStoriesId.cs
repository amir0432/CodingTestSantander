namespace BestHackerNews.Contracts
{
    public interface IBestStoriesId
    {
        public Task<IEnumerable<int>> GetBestStoriesIds();
    }
}
