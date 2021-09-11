using System;
using System.Globalization;
using Bookland.Extensions;
using Bookland.Modules;
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
                new SetMetadata(
                    "publishedDate",
                    Config.FromDocument(
                        doc =>
                        {
                            var postDetailsFromPath = doc.GetPostDetailsFromPath();
                            var date = $"{postDetailsFromPath["year"].Value}-{postDetailsFromPath["month"].Value}-{postDetailsFromPath["date"].Value}";
                            return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        })),
                new SetMetadata(
                    "slug",
                    Config.FromDocument(
                        doc =>
                        {
                            var postDetailsFromPath = doc.GetPostDetailsFromPath();
                            return postDetailsFromPath["slug"].Value;
                        })),
                new ReplaceInContent(@"!\[(?<alt>.*)\]\(./(?<imagePath>.*)\)", Config.FromDocument((document, context) => $"![$1](../assets/{document.GetString("slug")}/$2)")).IsRegex(),
                new GenerateReadingTime(),
                new RenderMarkdown().UseExtensions(),
                new OptimizeFileName(),
                new SetDestination(Config.FromDocument((doc, ctx) => new NormalizedPath("blog").Combine($"{doc.GetString("slug")}.html"))),
            };

            PostProcessModules = new ModuleList
            {
                new RenderRazor().WithModel(Config.FromDocument((document, context) => document.AsPost(context))),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
