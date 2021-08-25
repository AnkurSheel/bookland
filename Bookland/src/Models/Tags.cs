using System.Collections.Generic;
using Statiq.Common;

namespace Bookland.Models
{
    public record Tags : BaseModel
    {
        public Tags(IReadOnlyList<Tag> tags, IDocument document, IExecutionContext context) : base(document, context)
        {
            AllTags = tags;
        }

        public IReadOnlyList<Tag> AllTags { get; }
    }
}
