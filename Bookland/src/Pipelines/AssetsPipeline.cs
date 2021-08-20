using Bookland.Extensions;
using Statiq.Common;
using Statiq.Core;

namespace Bookland.Pipelines
{
    public class AssetsPipeline : Pipeline
    {
        public AssetsPipeline()
        {
            InputModules = new ModuleList
            {
                new CopyFiles("assets/**/*.{*,!scss,!css}"),
            };
        }
    }
}
