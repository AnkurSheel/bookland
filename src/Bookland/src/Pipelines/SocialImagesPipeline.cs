using System.Linq;
using Bookland.Modules;
using Bookland.Services;
using Statiq.Common;
using Statiq.Core;

namespace Bookland.Pipelines
{
    public class SocialImagesPipeline : Pipeline
    {
        private readonly IImageService _imageService;

        public SocialImagesPipeline(IImageService imageService)
        {
            _imageService = imageService;
            Dependencies.AddRange(nameof(PostPipeline));

            ProcessModules = new ModuleList
            {
                new ConcatDocuments(Dependencies.ToArray()),
                new GenerateSocialImages(_imageService)
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
