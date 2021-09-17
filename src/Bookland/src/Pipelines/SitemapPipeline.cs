using System;
using System.Linq;
using Bookland.Extensions;
using Bookland.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web;

namespace Bookland.Pipelines
{
    public class SitemapPipeline : Pipeline
    {
        public SitemapPipeline()
        {
            Dependencies.AddRange(nameof(HomePipeline), nameof(PagesPipeline), nameof(PostPipeline));
            ProcessModules = new ModuleList
            {
                new ConcatDocuments(Dependencies.ToArray()),
                new SetMetadata(
                    Keys.SitemapItem,
                    Config.FromDocument(
                        (document, context) =>
                        {
                            var sitemapItem = new SitemapItem($"{context.GetString("SiteUrl")}{document.GetLink()}");

                            if (document.ContainsKey(MetaDataKeys.PublishedDate))
                            {
                                var originalDate = document.GetDateTime(MetaDataKeys.PublishedDate);
                                var publishedDate = document.GetPublishedDate();

                                if (originalDate.Date <= publishedDate.Date)
                                {
                                    sitemapItem.LastModUtc = DateTime.SpecifyKind(publishedDate, DateTimeKind.Utc);
                                }
                            }

                            return sitemapItem;
                        })),
                new GenerateSitemap()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
