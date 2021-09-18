using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ByteSizeLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Spectre.Console.Cli;
using Statiq.App;
using Statiq.Common;

namespace Bookland.Commands
{
    public class ResizeJpegSettings : EngineCommandSettings
    {
        [CommandArgument(0, "<width>")]
        [Description("Specify the target width")]
        public int Width { get; set; }

        [CommandArgument(0, "<height>")]
        [Description("Specify the target height")]
        public int Height { get; set; }
    }

    [Description(
        "Resizes the jpegs. Passing zero for one of height or width within the resize options will automatically preserve the aspect ratio of the original image or the nearest possible ratio")]
    public class ResizeJpeg : EngineCommand<ResizeJpegSettings>
    {
        public ResizeJpeg(IConfiguratorCollection configurators, Settings settings, IServiceCollection serviceCollection, Bootstrapper bootstrapper) : base(
            configurators,
            settings,
            serviceCollection,
            bootstrapper)
        {
        }

        protected override async Task<int> ExecuteEngineAsync(CommandContext commandContext, ResizeJpegSettings commandSettings, IEngineManager engineManager)
        {
            var jpegs = engineManager.Engine.FileSystem.GetInputFiles("**/*.{jpg, jpeg}").Select(x => x.Path).ToList();
            var totalPre = 0l;
            var totalPost = 0l;

            engineManager.Engine.Logger.Log(LogLevel.Information, "Beginning resizing of images");

            foreach (var jpeg in jpegs)
            {
                var preSize = engineManager.Engine.FileSystem.GetFile(jpeg.FullPath).Length;
                totalPre += preSize;

                var image = await Image.LoadAsync(jpeg.FullPath);

                var originalSize = image.Size();
                var resizeOptions = new ResizeOptions
                {
                    Size = new Size(commandSettings.Width, commandSettings.Height),
                    Compand = true
                };

                image.Mutate(imageContext => imageContext.Resize(resizeOptions));

                await image.SaveAsJpegAsync(jpeg.FullPath);
                var postSize = engineManager.Engine.FileSystem.GetFile(jpeg.FullPath).Length;
                totalPost += postSize;

                var percentChanged = postSize > preSize
                    ? postSize / (decimal)preSize - 1
                    : 1 - postSize / (decimal)preSize;
                
                engineManager.Engine.Logger.Log(
                    LogLevel.Information,
                    "Resized {Path} with {Size}. Changed from {PreSize} to {PostSize} ({PercentChanged:P})",
                    jpeg.GetRelativeInputPath(),
                    originalSize,
                    ByteSize.FromBytes(preSize).ToString(),
                    ByteSize.FromBytes(postSize).ToString(),
                    percentChanged);
            }

            engineManager.Engine.Logger.Log(
                LogLevel.Information,
                "Resizing complete. Updated Size from {PreSize} to {PostSize}",
                ByteSize.FromBytes(totalPre).ToString(),
                ByteSize.FromBytes(totalPost).ToString());

            return 0;
        }
    }
}
