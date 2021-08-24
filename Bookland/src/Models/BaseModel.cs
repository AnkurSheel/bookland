using System.Collections.Generic;
using System.Linq;
using Statiq.Common;

namespace Bookland.Models
{
    public record BaseModel
    {
        public BaseModel(IDocument document, IExecutionContext context)
        {
            PageTitle = document.GetString("Title");
            NavigationLinks = context.OutputPages.GetChildrenOf("index.html")
                .Where(x => x.Destination != "index.html")
                .Select(x => new NavigationLink(x.GetString("title"), $"/{x.Destination.FileNameWithoutExtension.ToString()}"))
                .OrderBy(x => x.Title)
                .ToList();
            Script = context.GetLink("/assets/js/blog.js");
            SiteTitle = context.GetString("SiteTitle");
            Description = context.GetString("SiteDescription");
            PageUrl = $"{context.GetString("SiteUrl")}{document.GetLink()}";
            CanonicalUrl = context.GetString("canonicalUrl") ?? PageUrl;
        }

        public string PageUrl { get; set; }

        public string Description { get; set; }

        public string PageTitle { get; }

        public IReadOnlyList<NavigationLink> NavigationLinks { get; }

        public string Script { get; }

        public string SiteTitle { get; }

        public string CanonicalUrl { get; }
    }
}
