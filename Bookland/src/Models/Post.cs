using Statiq.Common;

namespace Bookland.Models
{
    public record Post : BaseModel
    {
        public Post(string title, string slug, IDocument document, IExecutionContext context) : base(document, context)
        {
            Title = title;
            Slug = slug;
        }

        public string Title { get; }

        public string Slug { get; }
    }
}
