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
                            var posts = context.Outputs.FromPipeline(nameof(PostPipeline)).Select(postDocument => postDocument.AsPost(context)).ToList();

                            return new Posts(posts, document, context);
                        })),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
