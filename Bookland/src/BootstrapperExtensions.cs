using System.IO;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web;
using Statiq.Web.Pipelines;

namespace Bookland
{
    public static class BootstrapperExtensions
    {
        public static Bootstrapper AddNpmProcesses(this Bootstrapper bootstrapper)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var nodeDirectory = Path.Combine(currentDirectory, "..", "node");
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
                    ProcessTiming.BeforeExecution,
                    _ => new ProcessLauncher(
                        "npx",
                        "tailwind",
                        "build",
                        $"-i {Path.Combine(currentDirectory, "input", "assets", "_site.css")}",
                        $"-o {Path.Combine(currentDirectory, "output", "assets", "styles.css")}")
                    {
                        LogErrors = false,
                        WorkingDirectory = nodeDirectory
                    });

            return bootstrapper;
        }

        public static Bootstrapper DisablePipelines(this Bootstrapper bootstrapper)
        {
            bootstrapper.ModifyPipeline(
                nameof(Inputs),
                pipeline =>
                {
                    pipeline.WithInputModules(new FilterDocuments(false));
                });
            // bootstrapper.ModifyPipeline(
            //         nameof(Content),
            //         pipeline =>
            //         {
            //             pipeline.WithProcessModules(new FilterDocuments(false));
            //         })
            //     .ModifyPipeline(
            //         nameof(Assets),
            //         pipeline =>
            //         {
            //             pipeline.WithProcessModules(new FilterDocuments(false));
            //         })

            return bootstrapper;
        }
    }
}
