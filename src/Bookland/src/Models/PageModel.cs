﻿using System.Collections.Generic;
using Statiq.Common;

namespace Bookland.Models
{
    public record PageModel : BaseModel
    {
        public PageModel(IDocument document, IExecutionContext context, IReadOnlyList<Post> posts) : base(document, context)
        {
            Posts = posts;
        }

        public IReadOnlyList<Post> Posts { get; }
    }
}