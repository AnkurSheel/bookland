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
                new ExecuteIf(Config.FromDocument(doc => doc.Source.Name.Contains("favicon.ico")))
                    {
                        new SetDestination("favicon.ico")
                    }.ElseIf(
                        Config.FromDocument(doc => doc.Source.FullPath.Contains("posts")),
                        new SetDestination(
                            Config.FromDocument(
                                doc =>
                                {
                                    var postDetailsFromPath = doc.GetPostDetailsFromPath();
                                    return new NormalizedPath("assets").Combine("images").Combine(postDetailsFromPath["slug"].ToString()).Combine(doc.Source.FileName);
                                })))
                    .Else(new SetDestination(Config.FromDocument(document => new NormalizedPath("assets").Combine("images").Combine(document.Source.FileName))))
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
