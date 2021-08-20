using Bookland.Extensions;
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
                new RenderRazor().WithBaseModel()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
