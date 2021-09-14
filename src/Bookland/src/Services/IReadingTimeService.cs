namespace Bookland.Services
{
    public interface IReadingTimeService
    {
        ReadingTimeData GetReadingTime(string content, int wordsPerMinute);
    }

    public record ReadingTimeData(int Minutes, int Seconds, int Words);
}
