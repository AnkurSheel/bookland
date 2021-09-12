using System.Threading.Tasks;
using Bookland.Extensions;
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
                .RunAsync();
    }
}
