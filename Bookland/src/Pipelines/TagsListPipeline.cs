using System.Linq;
using Bookland.Extensions;
using Bookland.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;

namespace Bookland.Pipelines
{
    public class TagsListPipeline : Pipeline
    {
        public TagsListPipeline()
        {
            Dependencies.Add(nameof(TagsPipeline));

            InputModules = new ModuleList
            {
                new ReadFiles("TagsList.cshtml")
            };

            ProcessModules = new ModuleList
            {
                new SetDestination("tags.html"),
                new SetMetadata("Title", "All Tags")
            };

            PostProcessModules = new ModuleList
            {
                new RenderRazor().WithModel(
                    Config.FromDocument(
                        (document, context) =>
                        {
                            var tags = context.Outputs.FromPipeline(nameof(TagsPipeline)).Select(x => x.AsTag(context)).OrderByDescending(x => x.Posts.Count).ThenBy(x => x.PageTitle).ToList();
                            return new Tags(tags, document, context);
                        })),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
