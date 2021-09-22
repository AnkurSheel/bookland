using System;
using System.Globalization;
using Bookland.Extensions;
using Bookland.Modules;
using Bookland.Services;
using Statiq.Common;
using Statiq.Core;
using Statiq.Markdown;
using Statiq.Razor;
using Statiq.Yaml;

namespace Bookland.Pipelines
{
    public class PostPipeline : Pipeline
    {
        public PostPipeline(IReadingTimeService readingTimeService)
        {
            InputModules = new ModuleList
            {
                new ReadFiles("posts/**/*.md")
            };

            ProcessModules = new ModuleList
            {
                new ExtractFrontMatter(new ParseYaml()),
                new GeneratePostDetailsFromPath(),
                new GenerateRssMetaData(),
                new ReplaceInContent(@"!\[(?<alt>.*)\]\(./(?<imagePath>.*)\)", Config.FromDocument((document, context) => $"![$1](../{Constants.PostImagesDirectory}/{document.GetString(MetaDataKeys.Slug)}/$2)")).IsRegex(),
                new GenerateReadingTime(readingTimeService),
                new RenderMarkdown().UseExtensions(),
                new OptimizeFileName(),
                new SetDestination(Config.FromDocument((doc, ctx) => new NormalizedPath("blog").Combine($"{doc.GetString("slug")}.html"))),
            };

            PostProcessModules = new ModuleList
            {
                new RenderRazor().WithBaseModel()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
