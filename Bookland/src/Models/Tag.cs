﻿using System.Collections.Generic;
using Statiq.Common;

namespace Bookland.Models
{
    public record Tag : BaseModel
    {
        public Tag(
            IDocument document,
            IExecutionContext context,
            string name,
            string url,
            IReadOnlyList<Post> posts) : base(document, context)
        {
            Name = name;
            Url = url;
            Posts = posts;
        }

        public string Name { get; }

        public string Url { get; }

        public IReadOnlyList<Post> Posts { get; }
    }
}
