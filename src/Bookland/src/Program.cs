using System.Threading.Tasks;
using Bookland.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using StatiqHelpers.ImageHelpers;
using StatiqHelpers.ReadingTimeModule;

namespace Bookland
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
            => await Bootstrapper.Factory.CreateWeb(args)
                .RemovePipelines()
                .AddNpmProcesses()
                .AddSetting(WebKeys.OutputPath, "../../output")
                .AddCommand<ResizeJpeg>()
                .ConfigureServices(
                    services =>
                    {
                        services.AddTransient<IImageService, ImageService>();
                        services.AddTransient<IReadingTimeService, ReadingTimeService>();
                    })
                .AddPipelines()
                .RunAsync();
    }
}
