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
            InputModules = new ModuleList
            {
                new ReadFiles("posts/**/*.md")
            };

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(
                    Config.FromContext(
                        context =>
                        {
                            return (IEnumerable<IDocument>)new[] { context.CreateDocument(new NormalizedPath("blog.html"), new[] { new KeyValuePair<string, object>(Keys.Children, context.Inputs) }) };
                        })),
                new SetMetadata("Title", "All Summaries")
            };

            PostProcessModules = new ModuleList
            {
                new MergeContent(new ReadFiles("Blog.cshtml")),
                new RenderRazor().WithModel(
                    Config.FromDocument(
                        (document, context) =>
                        {
                            var postDocuments = document.GetChildren();
                            var posts = postDocuments.Select(
                                    postDocument =>
                                    {
                                        var postDetailsFromPath = postDocument.GetPostDetailsFromPath();
                                        var title = postDocument.GetString("Title");
                                        return new Post(
                                            title,
                                            postDetailsFromPath["slug"].Value,
                                            document,
                                            context);
                                    })
                                .ToList();

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
