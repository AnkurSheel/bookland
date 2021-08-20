using System.Collections.Generic;
using Statiq.Common;

namespace Bookland.Models
{
    public record Posts : BaseModel
    {
        public Posts(IReadOnlyList<Post> posts, IDocument document, IExecutionContext context) : base(document, context)
        {
            AllPosts = posts;
        }

        public IReadOnlyList<Post> AllPosts { get; }
    }
}
