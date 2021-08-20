using Bookland.Models;
using Statiq.Common;
using Statiq.Razor;

namespace Bookland.Extensions
{
    public static class RazorExtensions
    {
        public static RenderRazor WithBaseModel(this RenderRazor renderRazor, string? title = null)
        {
            return renderRazor.WithModel(Config.FromDocument((document, context) => new BaseModel(document, context)));
        }
    }
}
