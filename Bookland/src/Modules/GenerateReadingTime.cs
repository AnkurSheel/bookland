using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Statiq.Common;

namespace Bookland.Modules
{
    public record ReadingTimeData(int Minutes, int Seconds, int Words);

    public class GenerateReadingTime : ParallelModule
    {
        private static readonly Regex SpacesRegex = new Regex(@"\S+", RegexOptions.Multiline);
        private readonly int _wordsPerMinute;

        public GenerateReadingTime() : this(200)
        {
        }

        public GenerateReadingTime(int wordsPerMinute)
        {
            _wordsPerMinute = wordsPerMinute;
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            var content = await input.GetContentStringAsync();
            return input.Clone(
                    new MetadataItems
                    {
                        { "ReadingTime", GetReadingTime(content) }
                    })
                .Yield();
        }

        private ReadingTimeData GetReadingTime(string content)
        {
            var words = SpacesRegex.Matches(content).Count;

            var minutes = words / _wordsPerMinute;
            var remainingWords = words % _wordsPerMinute;
            var seconds = remainingWords * 60 / _wordsPerMinute;

            return new ReadingTimeData(minutes, seconds, words);
        }
    }
}
