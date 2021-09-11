using System.Linq;
using System.Threading.Tasks;
using Bookland.Modules;
using Statiq.Common;
using Statiq.Testing;
using Xunit;

namespace Bookland.Unit.Tests
{
    public class GenerateReadingTimeTests : BaseFixture
    {
        [Theory]
        [InlineData(100, 0, 30)]
        [InlineData(200, 1, 0)]
        [InlineData(300, 1, 30)]
        [InlineData(400, 2, 0)]
        public async Task If_WPM_is_not_overridden_readingTime_MetaData_is_added_using_200WPM(int numberOfWords, int expectedMinutes, int expectedSeconds)
        {
            string input = string.Concat(Enumerable.Repeat("a ", numberOfWords));

            TestDocument document = new TestDocument(input);
            GenerateReadingTime readingTime = new GenerateReadingTime();

            TestDocument result = await ExecuteAsync(document, readingTime).SingleAsync();

            var readingTimeData = result.Get<ReadingTimeData>(MetaDataKeys.ReadingTime);

            Assert.Equal(new ReadingTimeData(expectedMinutes, expectedSeconds, numberOfWords), readingTimeData);
        }

        [Theory]
        [InlineData(400, 200, 0, 30)]
        [InlineData(300, 300, 1, 0)]
        public async Task ReadingTime_MetaData_is_added_correctly_when_WPM_is_overriden(int wordsPerMinute, int numberOfWords, int expectedMinutes, int expectedSeconds)
        {
            string input = string.Concat(Enumerable.Repeat("a ", numberOfWords));

            TestDocument document = new TestDocument(input);
            GenerateReadingTime readingTime = new GenerateReadingTime(wordsPerMinute);

            TestDocument result = await ExecuteAsync(document, readingTime).SingleAsync();

            var readingTimeData = result.Get<ReadingTimeData>(MetaDataKeys.ReadingTime);

            Assert.Equal(new ReadingTimeData(expectedMinutes, expectedSeconds, numberOfWords), readingTimeData);
        }
    }
}
