using Bookland.Extensions;
using Statiq.Common;
using Statiq.Core;

namespace Bookland.Pipelines
{
    public class ImagesPipeline : Pipeline
    {
        public ImagesPipeline()
        {
            InputModules = new ModuleList
            {
                new ReadFiles("**/*.{jpg,png,svg,ico}")
            };

            ProcessModules = new ModuleList
            {
                new SetDestination(
                    Config.FromDocument(
                        (doc, ctx) =>
                        {
                            var postDetailsFromPath = doc.GetPostDetailsFromPath();
                            return new NormalizedPath("assets").Combine(postDetailsFromPath["slug"].ToString()).Combine(doc.Source.FileName);
                        })),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
