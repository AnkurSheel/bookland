using System.Linq;
using System.Threading.Tasks;
using Bookland.Services;
using Xunit;

namespace Bookland.Unit.Tests
{
    public class ReadingTimeServiceTests
    {
        private readonly IReadingTimeService _readingTimeService;

        public ReadingTimeServiceTests()
        {
            _readingTimeService = new ReadingTimeService();
        }

        [Theory]
        [InlineData(100, 0, 30)]
        [InlineData(200, 1, 0)]
        [InlineData(300, 1, 30)]
        [InlineData(400, 2, 0)]
        public void If_WPM_is_not_overridden_readingTime_MetaData_is_added_using_200WPM(int numberOfWords, int expectedMinutes, int expectedSeconds)
        {
            string input = string.Concat(Enumerable.Repeat("a ", numberOfWords));

            var readingTimeData = _readingTimeService.GetReadingTime(input, 200);

            Assert.Equal(new ReadingTimeData(expectedMinutes, expectedSeconds, numberOfWords), readingTimeData);
        }

        [Theory]
        [InlineData(400, 200, 0, 30)]
        [InlineData(300, 300, 1, 0)]
        public void  ReadingTime_MetaData_is_added_correctly_when_WPM_is_overriden(int wordsPerMinute, int numberOfWords, int expectedMinutes, int expectedSeconds)
        {
            string input = string.Concat(Enumerable.Repeat("a ", numberOfWords));

            var readingTimeData = _readingTimeService.GetReadingTime(input, wordsPerMinute);

            Assert.Equal(new ReadingTimeData(expectedMinutes, expectedSeconds, numberOfWords), readingTimeData);
        }
    }
}
