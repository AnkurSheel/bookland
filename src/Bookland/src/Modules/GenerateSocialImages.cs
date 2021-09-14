using System.Collections.Generic;
using System.Threading.Tasks;
using Bookland.Extensions;
using Bookland.Services;
using Microsoft.Extensions.Logging;
using Statiq.Common;

namespace Bookland.Modules
{
    public class GenerateSocialImages : ParallelModule
    {
        private readonly IImageService _imageService;

        public GenerateSocialImages(IImageService imageService)
        {
            _imageService = imageService;
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            context.LogDebug($"Read file {input.Source}");
            var post = input.AsPost(context);

            var coverImagePath = $"{input.Source.Parent.FullPath}/{post.CoverImagePath}";

            var stream = await _imageService.CreateImageDocument(
                1200,
                630,
                coverImagePath,
                post.PageTitle,
                post.ReadingTimeData,
                post.SiteTitle);

            var facebookDoc = context.CreateDocument(input.Source, $"./assets/images/social/{input.Destination.FileNameWithoutExtension}-facebook.png", context.GetContentProvider(stream));

            context.LogDebug($"Created {facebookDoc.Destination}");

            stream = await _imageService.CreateImageDocument(
                440,
                220,
                coverImagePath,
                post.PageTitle,
                post.ReadingTimeData,
                post.SiteTitle);

            var twitterDoc = context.CreateDocument(input.Source, $"./assets/images/social/{input.Destination.FileNameWithoutExtension}-twitter.png", context.GetContentProvider(stream));

            context.LogDebug($"Created {twitterDoc.Destination}");

            return new[] { facebookDoc, twitterDoc };
        }
    }
}
