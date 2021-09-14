namespace Bookland.Services
{
    public interface IReadingTimeService
    {
        ReadingTimeData GetReadingTime(string content, int wordsPerMinute);
    }
}
