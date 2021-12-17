using System.IO;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace Bookland.Extensions
{
    public static class BootstrapperExtensions
    {
        public static Bootstrapper AddNpmProcesses(this Bootstrapper bootstrapper)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var nodeDirectory = Path.Combine(currentDirectory, "..", "..", "node");
            Directory.CreateDirectory(nodeDirectory);

            var inputDirectory = Path.Combine(currentDirectory, "input");
            bootstrapper.AddProcess(
                    ProcessTiming.Initialization,
                    _ => new ProcessLauncher("npm", "install")
                    {
                        LogErrors = false,
                        WorkingDirectory = nodeDirectory
                    })
                .AddProcess(
                    ProcessTiming.Initialization,
                    _ => new ProcessLauncher(
                        "npx",
                        "tailwind",
                        "build",
                        $"-i {Path.Combine(currentDirectory, "input", "assets", "_site.css")}",
                        $"-o {Path.Combine(currentDirectory, "input", "assets", "styles.css")}",
                        $"--minify")
                    {
                        LogErrors = false,
                        WorkingDirectory = nodeDirectory
                    });

            return bootstrapper;
        }
    }
}
