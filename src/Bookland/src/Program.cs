using System.Threading.Tasks;
using Bookland.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using StatiqHelpers.Extensions;
using StatiqHelpers.ImageHelpers;
using StatiqHelpers.Pipelines;

namespace Bookland
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
            => await Bootstrapper.Factory.CreateWeb(args)
                .RemovePipelines()
                .AddNpmProcesses()
                .AddSetting(WebKeys.OutputPath, "../../output")
                .AddSetting(WebKeys.CachePath, "../../cache")
                .AddCommand<ResizeJpeg>()
                .AddPipelines()
                .AddServices()
                .RunAsync();
    }
}
