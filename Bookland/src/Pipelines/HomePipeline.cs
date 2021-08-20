using Bookland.Extensions;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using Statiq.Yaml;

namespace Bookland.Pipelines
{
    public class HomePipeline : Pipeline
    {
        public HomePipeline()
        {
            InputModules = new ModuleList
            {
                new ReadFiles("Index.cshtml")
            };

            ProcessModules = new ModuleList
            {
                new ExtractFrontMatter(new ParseYaml()),
                new OptimizeFileName(),
                new SetDestination(".html"),
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
