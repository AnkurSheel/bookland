using System.Collections.Generic;
using Statiq.Common;

namespace Bookland.Models
{
    public record Tag : BaseModel
    {

        public Tag(
            IDocument document,
            IExecutionContext context,
            string name,
            IReadOnlyList<Post> posts) : base(document, context)
        {
            Name = name;
            Posts = posts;
        }

        public string Name { get; }

        public IReadOnlyList<Post> Posts { get; }
    }
}
