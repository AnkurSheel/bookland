using System.IO;
using Bookland.Pipelines;
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

        public static Bootstrapper RemovePipelines(this Bootstrapper bootstrapper)
        {
            bootstrapper.ConfigureEngine(
                engine =>
                {
                    engine.Pipelines.Remove(nameof(Assets));
                    engine.Pipelines.Add(nameof(Assets), new Pipeline());
                    engine.Pipelines.Remove(nameof(Content));
                    engine.Pipelines.Add(nameof(Content), new Pipeline());
                    engine.Pipelines.Remove(nameof(AnalyzeContent));
                    engine.Pipelines.Add(
                        nameof(AnalyzeContent),
                        new Pipeline
                        {
                            Deployment = true,
                            ExecutionPolicy = ExecutionPolicy.Normal,
                            InputModules =
                            {
                                new ReplaceDocuments(
                                    nameof(PostPipeline),
                                    nameof(Content),
                                    nameof(Archives),
                                    nameof(Assets))
                            }
                        });
                });

            return bootstrapper;
        }
    }
}
