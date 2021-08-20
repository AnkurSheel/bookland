﻿using Statiq.Common;

namespace Bookland.Models
{
    public record BaseModel
    {
        public BaseModel(IDocument document, IExecutionContext context)
        {
            Document = document;
            Context = context;
        }

        public IDocument Document { get; }

        public IExecutionContext Context { get; }
    }
}