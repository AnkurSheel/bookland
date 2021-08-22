using System.Collections.Generic;
using System.Linq;
using Bookland.Extensions;
using Bookland.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using Statiq.Yaml;

namespace Bookland.Pipelines
{
    public class PostListPipeline : Pipeline
    {
        public PostListPipeline()
        {
            Dependencies.Add(nameof(PostPipeline));

            InputModules = new ModuleList
            {
                new ReadFiles("Blog.cshtml")
            };

            ProcessModules = new ModuleList
            {
                new SetDestination("blog.html"),
                new SetMetadata("Title", "All Summaries")
            };

            PostProcessModules = new ModuleList
            {
                new RenderRazor().WithModel(
                    Config.FromDocument(
                        (document, context) =>
                        {
                            var postGroupedByYear = context.Outputs.FromPipeline(nameof(PostPipeline))
                                .GroupBy(x => x.GetDateTime("publishedDate").Year)
                                .OrderByDescending(x => x.Key)
                                .ToDictionary(
                                    grouping => grouping.Key,
                                    grouping => grouping.OrderByDescending(x => x.GetDateTime("publishedDate")).Select(postDocument => postDocument.AsPost(context)).ToList());
                            return new Posts(postGroupedByYear, document, context);
                        })),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
