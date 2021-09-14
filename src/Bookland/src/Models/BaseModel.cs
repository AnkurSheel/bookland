using System.Collections.Generic;
using System.Linq;
using Bookland.Modules;
using Statiq.Common;

namespace Bookland.Models
{
    public record BaseModel
    {
        public BaseModel(IDocument document, IExecutionContext context)
        {
            PageTitle = document.GetString(MetaDataKeys.Title);
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
            TwitterUserName = context.GetString("TwitterUsername");
        }

        public string PageUrl { get; set; }

        public string Description { get; set; }

        public string PageTitle { get; }

        public IReadOnlyList<NavigationLink> NavigationLinks { get; }

        public string Script { get; }

        public string SiteTitle { get; }

        public string CanonicalUrl { get; }

        public string? ImageTwitter { get; protected set; }

        public string? ImageFacebook { get; protected set; }

        public string TwitterUserName { get; set; }
    }
}
