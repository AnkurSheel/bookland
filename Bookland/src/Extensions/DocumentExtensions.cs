using System.Linq;
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
            var authorDocuments = document.GetDocumentList("authors");
            var authors = authorDocuments.Select(authorDocument => new Author(authorDocument.GetString("name"), authorDocument.GetString("link"))).ToList();

            return new Post(
                document,
                context,
                document.GetString("slug"),
                document.GetDateTime("publishedDate").ToString("dd-MMM-yyyy"),
                document.GetPublishedDate().ToString("dd-MMM-yyyy"),
                document.GetString("bookTitle"),
                document.GetString("amazonLink"),
                document.GetInt("pages"),
                authors,
                document.GetString("coverImage"),
                document.GetInt("rating"),
                document.GetList<string>("tags"));
        }
    }
}
