using System.Text.RegularExpressions;
using Bookland.Models;
using Statiq.Common;
using Statiq.Web;

namespace Bookland.Extensions
{
    public static class DocumentExtensions
    {
        public static GroupCollection GetPostDetailsFromPath(this IDocument doc)
        {
            var regex = new Regex(@".*(?<year>[\d]{4})-(?<month>[\d]{2})-(?<date>[\d]{2})-(?<slug>.+)$");
            var m = regex.Match(doc.Source.Parent.ToString());
            return m.Groups;
        }

        public static Post AsPost(this IDocument document, IExecutionContext context)
        {
            var title = document.GetString("Title");
            var slug = document.GetString("slug");
            var publishedDate = document.GetDateTime("publishedDate").ToString("dd-MMM-yyyy");
            var updatedDate = document.GetPublishedDate().ToString("dd-MMM-yyyy");
            return new Post(
                title,
                slug,
                publishedDate,
                updatedDate,
                document,
                context);
        }
    }
}
