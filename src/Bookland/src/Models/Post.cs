using System;
using System.Collections.Generic;
using Bookland.Modules;
using Statiq.Common;

namespace Bookland.Models
{
    public record Post : BaseModel
    {
        public Post(
            IDocument document,
            IExecutionContext context,
            string excerpt,
            string slug,
            DateTime publishedDate,
            DateTime updatedDate,
            string bookTitle,
            string amazonLink,
            int pages,
            IReadOnlyList<Author> authors,
            string coverImagePath,
            int rating,
            IReadOnlyList<string> tags,
            ReadingTimeData readingTimeData) : base(document, context)
        {
            Description = excerpt;
            Slug = slug;
            PublishedDate = publishedDate;
            UpdatedDate = updatedDate;
            BookTitle = bookTitle;
            AmazonLink = amazonLink;
            Pages = pages;
            Authors = authors;
            CoverImagePath = coverImagePath;
            Rating = rating;
            Tags = tags;
            ReadingTimeData = readingTimeData;
            Url = $"/blog/{slug}";
            CoverImageLink = $"/assets/images/{slug}/{coverImagePath}";
            ImageFacebook = $"{context.GetString("SiteUrl")}/assets/social/{slug}-facebook.png";
            ImageTwitter = $"{context.GetString("SiteUrl")}/assets/social/{slug}-twitter.png";
        }

        public string Url { get; }

        public string Slug { get; }

        public DateTime PublishedDate { get; }

        public DateTime UpdatedDate { get; }

        public string BookTitle { get; }

        public string AmazonLink { get; }

        public int Pages { get; }

        public IReadOnlyList<Author> Authors { get; }

        public string CoverImagePath { get; }

        public string CoverImageLink { get; }

        public int Rating { get; }

        public IReadOnlyList<string> Tags { get; }

        public ReadingTimeData ReadingTimeData { get; }
    }

    public class Author
    {
        public Author(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public string Name { get; }

        public string Link { get; }
    }
}
