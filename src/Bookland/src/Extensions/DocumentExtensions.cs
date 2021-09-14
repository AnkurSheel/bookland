using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Bookland.Models;
using Bookland.Modules;
using Bookland.Services;
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
            var authorDocuments = document.GetDocumentList(MetaDataKeys.Authors);
            var authors = authorDocuments.Select(authorDocument => new Author(authorDocument.GetString("name"), authorDocument.GetString("link"))).ToList();

            return new Post(
                document,
                context,
                document.GetString(MetaDataKeys.Excerpt),
                document.GetString(MetaDataKeys.Slug),
                document.GetDateTime(MetaDataKeys.PublishedDate),
                document.GetPublishedDate(),
                document.GetString(MetaDataKeys.BookTitle),
                document.GetString(MetaDataKeys.AmazonLink),
                document.GetInt(MetaDataKeys.Pages),
                authors,
                document.GetString(MetaDataKeys.CoverImage).TrimStart('.', '/'),
                document.GetInt(MetaDataKeys.Rating),
                document.GetList<string>(MetaDataKeys.Tags),
                document.Get<ReadingTimeData>(MetaDataKeys.ReadingTime));
        }

        public static Tag AsTag(this IDocument document, IExecutionContext context)
        {
            var posts = document.GetChildren().Select(x => x.AsPost(context)).OrderByDescending(x => x.PublishedDate).ToList();

            var name = document.GetString(MetaDataKeys.Name);
            return new Tag(document, context, name, new NormalizedPath($"/tags/{name}").OptimizeFileName().ToString(), posts);
        }

        public static HomeModel AsHomeModel(this IDocument document, IExecutionContext context, IReadOnlyList<Post> posts)
            => new HomeModel(document, context, posts);
    }
}
