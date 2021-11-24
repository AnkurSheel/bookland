using System.Threading.Tasks;
using Bookland.Extensions;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using StatiqHelpers.Extensions;

namespace Bookland
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
            => await Bootstrapper.Factory.InitStatiq(args)
                .AddNpmProcesses()
                .AddSetting(WebKeys.OutputPath, "../../output")
                .AddSetting(WebKeys.CachePath, "../../cache")
                .RunAsync();
    }
}
