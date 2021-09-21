using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Bookland.Models;
using Bookland.Modules;
using Bookland.Services;
using Statiq.Common;

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

        public static string GetExcerpt(this IDocument document)
            => document.GetString(MetaDataKeys.Excerpt);

        public static string GetSlug(this IDocument document)
            => document.GetString(MetaDataKeys.Slug);

        public static DateTime GetPublishedDate(this IDocument document)
            => document.GetDateTime(MetaDataKeys.PublishedDate);

        public static DateTime GetUpdatedTime(this IDocument document)
            => document.GetPublishedDate();

        public static string GetBookTitle(this IDocument document)
            => document.GetString(MetaDataKeys.BookTitle);

        public static string GetAmazonLink(this IDocument document)
            => document.GetString(MetaDataKeys.AmazonLink);

        public static int GetNumberOfPages(this IDocument document)
            => document.GetInt(MetaDataKeys.Pages);

        public static string GetCoverImagePath(this IDocument document)
            => document.GetString(MetaDataKeys.CoverImage).TrimStart('.', '/');

        public static string GetPageUrl(this IDocument document)
            => document.GetLink(true);

        public static string GetCoverImageLink(this IDocument document)
            => $"/{Constants.PostImagesDirectory}/{document.GetSlug()}/{document.GetCoverImagePath()}";

        public static int GetRating(this IDocument document)
            => document.GetInt(MetaDataKeys.Rating);

        public static IReadOnlyList<string> GetTags(this IDocument document)
            => document.GetList<string>(MetaDataKeys.Tags);

        public static ReadingTimeData GetReadingTime(this IDocument document)
            => document.Get<ReadingTimeData>(MetaDataKeys.ReadingTime);

        public static IReadOnlyList<Author> GetAuthors(this IDocument document)
        {
            var authorDocuments = document.GetDocumentList(MetaDataKeys.Authors);
            return authorDocuments.Select(authorDocument => new Author(authorDocument.GetString("name"), authorDocument.GetString("link"))).ToList();
        }

        public static string GetImageFacebook(this IDocument document)
            => IExecutionContext.Current.GetLink($"{Constants.SocialImagesDirectory}/{document.GetSlug()}-facebook.png", true);

        public static string GetImageTwitter(this IDocument document)
            => IExecutionContext.Current.GetLink($"{Constants.SocialImagesDirectory}/{document.GetSlug()}-twitter.png", true);

        public static Tag AsTag(this IDocument document, IExecutionContext context)
        {
            var posts = document.GetChildren().OrderByDescending(x => x.GetPublishedDate()).Select(x => x.AsBaseModel(context)).ToList();

            var name = document.GetString(MetaDataKeys.Name);
            return new Tag(document, context, name, new NormalizedPath($"/tags/{name}").OptimizeFileName().ToString(), posts);
        }

        public static PageModel AsPagesModel(this IDocument document, IExecutionContext context, IReadOnlyList<IDocument> posts)
            => new PageModel(document, context, posts);

        public static BaseModel AsBaseModel(this IDocument document, IExecutionContext context)
            => new BaseModel(document, context);
    }
}
