using Bookland.Extensions;
using Bookland.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Markdown;
using Statiq.Razor;
using Statiq.Yaml;

namespace Bookland.Pipelines
{
    public class PagesPipeline : Pipeline
    {
        public PagesPipeline()
        {
            InputModules = new ModuleList
            {
                new ReadFiles("pages/**/*.md")
            };

            ProcessModules = new ModuleList
            {
                new ExtractFrontMatter(new ParseYaml()),
                new RenderMarkdown().UseExtensions(),
                new OptimizeFileName(),
                new SetDestination(Config.FromDocument(
                    (doc, ctx) => doc.Destination.FileName.ChangeExtension(".html"))),
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
