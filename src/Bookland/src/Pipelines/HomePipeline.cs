using System.Linq;
using Bookland.Extensions;
using Bookland.Models;
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
            Dependencies.AddRange(nameof(PostPipeline));
            InputModules = new ModuleList
            {
                new ReadFiles("Index.cshtml")
            };

            ProcessModules = new ModuleList
            {
                new OptimizeFileName(),
                new SetMetadata("Title", Config.FromContext(context => context.GetString("SiteTitle"))),
                new SetDestination(".html"),
            };

            PostProcessModules = new ModuleList
            {
                new RenderRazor().WithModel(Config.FromDocument(((document, context) =>
                {
                    var posts = context.Outputs.FromPipeline(nameof(PostPipeline)).Select(x => x.AsPost(context)).ToList();
                    return document.AsHomeModel(context, posts);
                })))
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
