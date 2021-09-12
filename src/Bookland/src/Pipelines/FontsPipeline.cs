using Statiq.Common;
using Statiq.Core;
using Statiq.Minification;

namespace Bookland.Pipelines
{
    public class FontsPipeline : Pipeline
    {
        public FontsPipeline()
        {
            InputModules = new ModuleList
            {
                new ReadFiles("assets/**/*.ttf")
            };

            ProcessModules = new ModuleList
            {
                new SetDestination(Config.FromDocument(document => new NormalizedPath($"assets/fonts/{document.Source.FileName}"))),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
