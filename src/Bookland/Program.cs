using StatiqHelpers.CustomExtensions;

namespace Bookland
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
            => await Bootstrapper.Factory.CreateBootstrapper(args)
                .AddSetting(WebKeys.OutputPath, "../../output")
                .AddSetting(WebKeys.CachePath, "../../cache")
                .AddSetting(WebKeys.InputPaths, new[] { "input", "content" })
                .RunAsync();
    }
}
