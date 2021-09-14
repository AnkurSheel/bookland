using System.Threading.Tasks;
using Bookland.Extensions;
using Bookland.Services;
using Microsoft.Extensions.DependencyInjection;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace Bookland
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
            => await Bootstrapper.Factory.CreateWeb(args)
                .RemovePipelines()
                .AddNpmProcesses()
                .AddSetting(WebKeys.OutputPath, "../../output")
                .ConfigureServices(services =>
                {
                    services.AddTransient<IImageService, ImageService>();
                    services.AddTransient<IReadingTimeService, ReadingTimeService>();
                })
                .RunAsync();
    }
}
