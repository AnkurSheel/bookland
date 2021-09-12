using System;
using System.Globalization;
using System.Linq;
using Bookland.Extensions;
using Bookland.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web;
using Statiq.Web.Modules;

namespace Bookland.Pipelines
{
    public class SocialImagesPipeline : Pipeline
    {
        public SocialImagesPipeline()
        {
            Dependencies.AddRange(nameof(PostPipeline));

            ProcessModules = new ModuleList
            {
                new ConcatDocuments(Dependencies.ToArray()),
                new GenerateSocialImages()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
