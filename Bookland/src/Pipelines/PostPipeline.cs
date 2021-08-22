using System;
using System.Globalization;
using Bookland.Extensions;
using Bookland.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Markdown;
using Statiq.Razor;
using Statiq.Yaml;

namespace Bookland.Pipelines
{
    public class PostPipeline : Pipeline
    {
        public PostPipeline()
        {
            InputModules = new ModuleList
            {
                new ReadFiles("posts/**/*.md")
            };

            ProcessModules = new ModuleList
            {
                new ExtractFrontMatter(new ParseYaml()),
                new ReplaceInContent(@"!\[(?<alt>.*)\]\(./(?<imagePath>.*)\)", Config.FromDocument((document, context) =>
                {
                    var postDetailsFromPath = document.GetPostDetailsFromPath();
                    return $"![$1](../assets/{postDetailsFromPath["slug"]}/$2)";
                })).IsRegex(),
                new RenderMarkdown().UseExtensions(),
                new OptimizeFileName(),
                new SetDestination(
                    Config.FromDocument(
                        (doc, ctx) =>
                        {
                            var postDetailsFromPath = doc.GetPostDetailsFromPath();
                            return new NormalizedPath("blog").Combine($"{postDetailsFromPath["slug"]}.html");
                        })),
            };

            PostProcessModules = new ModuleList
            {
                new RenderRazor().WithModel(
                    Config.FromDocument(
                        (document, context) =>
                        {
                            var postDetailsFromPath = document.GetPostDetailsFromPath();

                            var title = document.GetString("Title");
                            var slug = new NormalizedPath("blog").Combine($"{postDetailsFromPath["slug"]}.html").ToString();
                            return new Post(
                                title,
                                slug,
                                document,
                                context);
                        })),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
