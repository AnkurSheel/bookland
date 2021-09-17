using System.Collections.Generic;
using System.Threading.Tasks;
using Bookland.Extensions;
using Microsoft.Extensions.Logging;
using Statiq.Common;
using Statiq.Feeds;

namespace Bookland.Modules
{
    public class GenerateRssMetaData : ParallelModule
    {
        protected override Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            context.LogDebug($"Read file {input.Source}");

            var post = input.AsPost(context);

            return Task.FromResult(
                input.Clone(
                        new MetadataItems
                        {
                            { FeedKeys.Description, post.Description },
                            { FeedKeys.Published, post.PublishedDate },
                            { FeedKeys.Image, post.CoverImageLink }
                        })
                    .Yield());
        }
    }
}
