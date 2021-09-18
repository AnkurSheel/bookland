using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ByteSizeLib;
using LibGit2Sharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Spectre.Console.Cli;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

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

        [CommandOption("-c")]
        [Description("Compress all files, not just uncommitted files")]
        public bool AllFiles { get; set; }
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
            Engine engine = engineManager.Engine;
            var jpegs = GetImages(!commandSettings.AllFiles, engine.FileSystem);

            var message = commandSettings.AllFiles
                ? "all files"
                : "checked out files";
            engineManager.Engine.Logger.Log(LogLevel.Information, "Beginning resizing of images on {message} : {count}", message, jpegs.Count);

            await ResizeImages(jpegs, commandSettings.Width, commandSettings.Height, engineManager.Engine);

            return 0;
        }

        private List<NormalizedPath> GetImages(bool onlyCheckedOutFiles, IFileSystem fileSystem)
        {
            var jpegs = fileSystem.GetInputFiles("**/*.{jpg, jpeg}").Select(x => x.Path).ToList();

            if (onlyCheckedOutFiles)
            {
                var rootPath = fileSystem.RootPath.Parent.Parent.FullPath;
                using var repo = new Repository(rootPath);
                var status = repo.RetrieveStatus();

                var modifiedJpegs = status.Where(x => Path.GetExtension(x.FilePath) == ".jpg" && x.State != FileStatus.Ignored)
                    .Select(x => new NormalizedPath(Path.Combine(rootPath, x.FilePath)))
                    .ToList();

                jpegs = jpegs.Where(x => modifiedJpegs.Contains(x)).ToList();
            }

            return jpegs;
        }

        private async Task ResizeImages(IReadOnlyList<NormalizedPath> jpegs, int newWidth, int newHeight, IEngine engine)
        {
            var totalPre = 0l;
            var totalPost = 0l;

            foreach (var jpeg in jpegs)
            {
                var preSize = engine.FileSystem.GetFile(jpeg.FullPath).Length;
                totalPre += preSize;

                var image = await Image.LoadAsync(jpeg.FullPath);

                var originalSize = image.Size();
                var resizeOptions = new ResizeOptions
                {
                    Size = new Size(newWidth, newHeight),
                    Compand = true
                };

                image.Mutate(imageContext => imageContext.Resize(resizeOptions));

                await image.SaveAsJpegAsync(jpeg.FullPath);
                var postSize = engine.FileSystem.GetFile(jpeg.FullPath).Length;
                totalPost += postSize;

                var percentChanged = postSize > preSize
                    ? postSize / (decimal)preSize - 1
                    : 1 - postSize / (decimal)preSize;

                engine.Logger.Log(
                    LogLevel.Information,
                    "Resized {Path} with {Size}. Changed from {PreSize} to {PostSize} ({PercentChanged:P})",
                    jpeg.GetRelativeInputPath(),
                    originalSize,
                    ByteSize.FromBytes(preSize).ToString(),
                    ByteSize.FromBytes(postSize).ToString(),
                    percentChanged);
            }

            engine.Logger.Log(LogLevel.Information, "Resizing complete. Updated Size from {PreSize} to {PostSize}", ByteSize.FromBytes(totalPre).ToString(), ByteSize.FromBytes(totalPost).ToString());
        }
    }
}
