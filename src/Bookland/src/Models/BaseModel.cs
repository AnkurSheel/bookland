using System.Collections.Generic;
using System.Linq;
using Bookland.Modules;
using Statiq.Common;
using Statiq.Web;

namespace Bookland.Models
{
    public record BaseModel
    {
        public BaseModel(IDocument document, IExecutionContext context)
        {
            var navigationDocuments = document.GetDocumentList("HeaderLinks");

            PageTitle = document.GetString(MetaDataKeys.Title);
            NavigationLinks = navigationDocuments.Select(x => new NavigationLink(x.GetString("Title"), x.GetString("Url"))).ToList();
            Script = context.GetLink($"/{Constants.JsDirectory}/blog.js");
            SiteTitle = context.GetString(Keys.Title);
            Description = context.GetString(WebKeys.Description);
            PageUrl = document.GetLink(true);
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
