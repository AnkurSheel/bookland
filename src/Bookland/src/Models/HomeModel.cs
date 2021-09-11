using System.Collections.Generic;
using Statiq.Common;

namespace Bookland.Models
{
    public record HomeModel : BaseModel
    {
        public HomeModel(IDocument document, IExecutionContext context, IReadOnlyList<Post> posts) : base(document, context)
        {
            Posts = posts;
        }

        public IReadOnlyList<Post> Posts { get; }
    }
}
