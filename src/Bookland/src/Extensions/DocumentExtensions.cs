using System.Collections.Generic;
using System.Linq;
using Bookland.Models;
using Statiq.Common;

namespace Bookland.Extensions
{
    public static class DocumentExtensions
    {
        public static string GetBookTitle(this IDocument document)
            => document.GetString(MetaDataKeys.BookTitle);

        public static string GetAmazonLink(this IDocument document)
            => document.GetString(MetaDataKeys.AmazonLink);

        public static int GetNumberOfPages(this IDocument document)
            => document.GetInt(MetaDataKeys.Pages);

        public static int GetRating(this IDocument document)
            => document.GetInt(MetaDataKeys.Rating);

        public static IReadOnlyList<Author> GetAuthors(this IDocument document)
        {
            var authorDocuments = document.GetDocumentList(MetaDataKeys.Authors);
            return authorDocuments.Select(authorDocument => new Author(authorDocument.GetString("name"), authorDocument.GetString("link"))).ToList();
        }
    }
}
