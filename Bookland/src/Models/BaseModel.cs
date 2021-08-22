using System.Collections.Generic;
using System.Linq;
using Statiq.Common;

namespace Bookland.Models
{
    public record BaseModel
    {
        public BaseModel(IDocument document, IExecutionContext context)
        {
            Title = document.GetString("Title");
            NavigationLinks = context.OutputPages.GetChildrenOf("index.html").Where(x => x.Destination != "index.html").ToList();
            Script = context.GetLink("/assets/js/blog.js");
        }

        public string Title { get; }

        public IReadOnlyList<IDocument> NavigationLinks { get; }

        public string Script { get; }
    }
}
