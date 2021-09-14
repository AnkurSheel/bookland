using System.Collections.Generic;
using System.Threading.Tasks;
using Bookland.Services;
using Microsoft.Extensions.Logging;
using Statiq.Common;

namespace Bookland.Modules
{
    public class GenerateReadingTime : ParallelModule
    {
        private readonly int _wordsPerMinute;
        private readonly IReadingTimeService _readingTimeService;

        public GenerateReadingTime(IReadingTimeService readingTimeService, int wordsPerMinute = 200)
        {
            _wordsPerMinute = wordsPerMinute;
            _readingTimeService = readingTimeService;
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            context.LogDebug($"Read file {input.Source}");

            using var textReader = input.GetContentTextReader();
            var content = await textReader.ReadToEndAsync();

            return input.Clone(
                    new MetadataItems
                    {
                        { MetaDataKeys.ReadingTime, _readingTimeService.GetReadingTime(content, _wordsPerMinute) }
                    })
                .Yield();
        }
    }
}
