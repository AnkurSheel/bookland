using System;
using System.Globalization;
using System.Linq;
using Bookland.Extensions;
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
                            var postDetailsFromPath = document.GetPostDetailsFromPath();

                            var sitemapItem = new SitemapItem($"{context.GetString("SiteUrl")}{document.GetLink()}");

                            if (postDetailsFromPath.Count > 1)
                            {
                                var date = $"{postDetailsFromPath["year"].Value}-{postDetailsFromPath["month"].Value}-{postDetailsFromPath["date"].Value}";
                                var originalDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
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
