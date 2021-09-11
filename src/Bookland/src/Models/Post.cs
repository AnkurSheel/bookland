using System;
using System.Collections.Generic;
using Bookland.Modules;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Statiq.Common;

namespace Bookland.Models
{
    public record Post : BaseModel
    {
        public Post(
            IDocument document,
            IExecutionContext context,
            string excerpt,
            string url,
            DateTime publishedDate,
            DateTime updatedDate,
            string bookTitle,
            string amazonLink,
            int pages,
            IReadOnlyList<Author> authors,
            string coverImageLink,
            int rating,
            IReadOnlyList<string> tags,
            ReadingTimeData readingTimeData) : base(document, context)
        {
            Description = excerpt;
            Url = url;
            PublishedDate = publishedDate;
            UpdatedDate = updatedDate;
            BookTitle = bookTitle;
            AmazonLink = amazonLink;
            Pages = pages;
            Authors = authors;
            CoverImageLink = coverImageLink;
            Rating = rating;
            Tags = tags;
            ReadingTimeData = readingTimeData;
        }

        public string Url { get; }

        public DateTime PublishedDate { get; }

        public DateTime UpdatedDate { get; }

        public string BookTitle { get; }

        public string AmazonLink { get; }

        public int Pages { get; }

        public IReadOnlyList<Author> Authors { get; }

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
