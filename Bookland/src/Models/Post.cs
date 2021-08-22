using Statiq.Common;

namespace Bookland.Models
{
    public record Post : BaseModel
    {
        public Post(
            string title,
            string slug,
            string publishedDate,
            string updatedDate,
            IDocument document,
            IExecutionContext context) : base(document, context)
        {
            Title = title;
            Slug = slug;
            PublishedDate = publishedDate;
            UpdatedDate = updatedDate;
        }

        public string Title { get; }

        public string Slug { get; }

        public string PublishedDate { get; }

        public string UpdatedDate { get; }
    }
}
