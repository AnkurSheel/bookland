using System.Collections.Generic;
using System.Linq;
using Bookland.Models;
using Statiq.Common;
using Statiq.Web;

namespace Bookland.Extensions
{
    public static class ContextExtensions
    {
        public static string GetSiteTitle(this IExecutionContext context)
            => context.GetString(Keys.Title);

        public static IReadOnlyList<NavigationLink> GetNavigationLinks(this IExecutionContext context)
            => context.OutputPages.GetChildrenOf("index.html")
                .Where(x => x.Destination != "index.html")
                .Select(x => new NavigationLink(x.GetString("title"), $"/{x.Destination.FileNameWithoutExtension.ToString()}"))
                .OrderBy(x => x.Title)
                .ToList();

        public static string GetScript(this IExecutionContext context)
            => context.GetLink($"/{Constants.JsDirectory}/blog.js");

        public static string GetDescription(this IExecutionContext context)
            => context.GetString(WebKeys.Description);

        public static string GetCanonicalUrl(this IExecutionContext context, IDocument document)
            => context.GetString("canonicalUrl") ?? document.GetPageUrl();

        public static string GetTwitterUserName(this IExecutionContext context)
            => context.GetString("TwitterUsername");
    }
}
