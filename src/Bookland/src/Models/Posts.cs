using System.Collections.Generic;
using Statiq.Common;

namespace Bookland.Models
{
    public record Posts : BaseModel
    {
        public Posts(IReadOnlyDictionary<int, List<BaseModel>> posts, IDocument document, IExecutionContext context) : base(document, context)
        {
            AllPosts = posts;
        }

        public IReadOnlyDictionary<int, List<BaseModel>> AllPosts { get; }
    }
}
