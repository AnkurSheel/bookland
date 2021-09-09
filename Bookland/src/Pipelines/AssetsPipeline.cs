﻿using System.Threading.Tasks;
using Bookland.Extensions;
using Statiq.Common;
using Statiq.Core;

namespace Bookland.Pipelines
{
    public class AssetsPipeline : Pipeline
    {
        public AssetsPipeline()
        {
            InputModules = new ModuleList
            {
                new CopyFiles("assets/**/*.{*,!scss,!css}"),
                new CopyFiles("assets/js/sw.js").To(file => Task.FromResult(new NormalizedPath("./sw.js"))),
                new ReadFiles("posts/**/*.{jpg,png}")
            };

            ProcessModules = new ModuleList
            {
                new SetDestination(
                    Config.FromDocument(
                        (doc, ctx) =>
                        {
                            var postDetailsFromPath = doc.GetPostDetailsFromPath();
                            return new NormalizedPath("assets").Combine(postDetailsFromPath["slug"].ToString()).Combine(doc.Source.FileName);
                        })),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
