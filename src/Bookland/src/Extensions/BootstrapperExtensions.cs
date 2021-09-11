﻿using System.IO;
using Bookland.Pipelines;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using Statiq.Feeds.Syndication;
using Statiq.Web;
using Statiq.Web.Pipelines;

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
                        $"-o {Path.Combine(currentDirectory, "input", "assets", "styles.css")}")
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
                    engine.Pipelines.Remove(nameof(Inputs));
                    engine.Pipelines.Remove(nameof(Assets));
                    engine.Pipelines.Remove(nameof(Content));
                    engine.Pipelines.Remove(nameof(Sitemap));
                    engine.Pipelines.Remove(nameof(Archives));
                    engine.Pipelines.Remove(nameof(Feeds));
                    engine.Pipelines.Remove(nameof(Data));
                    engine.Pipelines.Remove(nameof(Redirects));
                    engine.Pipelines.Remove(nameof(SearchIndex));
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
                                    nameof(ImagesPipeline),
                                    nameof(ScriptsPipeline),
                                    nameof(HomePipeline),
                                    nameof(PagesPipeline),
                                    nameof(PostListPipeline),
                                    nameof(PostPipeline),
                                    nameof(TagsListPipeline),
                                    nameof(TagsPipeline),
                                    nameof(SitemapPipeline))
                            }
                        });
                });

            return bootstrapper;
        }
    }
}