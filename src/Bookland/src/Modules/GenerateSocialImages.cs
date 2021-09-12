using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Bookland.Extensions;
using Bookland.Models;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Statiq.Common;

namespace Bookland.Modules
{
    public class GenerateSocialImages : ParallelModule
    {
        private readonly FontFamily _fonts;

        public GenerateSocialImages()
        {
            _fonts = new FontCollection().Install("./input/assets/fonts/Inter-VariableFont.ttf");
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            var post = input.AsPost(context);

            var facebookDoc = await CreateImageDocument(
                input,
                context,
                1200,
                630,
                post,
                "facebook");
            var twitterDoc = await CreateImageDocument(
                input,
                context,
                440,
                220,
                post,
                "twitter");

            return new[] { facebookDoc, twitterDoc };
        }

        private async Task<IDocument> CreateImageDocument(
            IDocument input,
            IExecutionContext context,
            int width,
            int height,
            Post post,
            string suffix)
        {
            using Image template = new Image<Rgb24>(width, height);
            using Image thumbnail = await Image.LoadAsync($"{input.Source.Parent.FullPath}/{post.CoverImagePath}");

            thumbnail.Mutate(
                imageContext =>
                {
                    imageContext.SetGraphicsOptions(
                        new GraphicsOptions()
                        {
                            Antialias = true
                        });
                    ResizeImage(width, height, imageContext);
                    DarkenImage(imageContext);
                });

            template.Mutate(
                imageContext =>
                {
                    AddGradient(width, height, imageContext);
                    imageContext.DrawImage(thumbnail, new Point(0, 0), 1f);
                    AddCenterText(imageContext, width, height, post);
                    AddBrand(imageContext, width, height, post);
                });

            Stream output = new MemoryStream();

            await template.SaveAsPngAsync(output);

            var destination = $"./assets/social/{input.Destination.FileNameWithoutExtension}-{suffix}.png";

            var doc = context.CreateDocument(input.Source, destination, context.GetContentProvider(output));
            return doc;
        }

        private static void AddGradient(int width, int height, IImageProcessingContext imageContext)
        {
            imageContext.Fill(
                new LinearGradientBrush(
                    new PointF(0, 0),
                    new PointF(width / 2, height / 2),
                    GradientRepetitionMode.Reflect,
                    new ColorStop(0f, Color.ParseHex("#16222A")),
                    new ColorStop(0.5f, Color.ParseHex("#3A6073"))));
        }

        private static void DarkenImage(IImageProcessingContext imageContext)
        {
            imageContext.Lightness(0.5f);
        }

        private static void ResizeImage(int width, int height, IImageProcessingContext imageContext)
        {
            imageContext.Resize(
                new ResizeOptions
                {
                    Position = AnchorPositionMode.Center,
                    Size = new Size(width, height),
                    Mode = ResizeMode.Pad,
                });
        }

        private void AddCenterText(IImageProcessingContext imageContext, int imageWidth, int imageHeight, Post post)
        {
            var fontSize = imageHeight / 10;
            var titleFont = new Font(_fonts, fontSize, FontStyle.Bold);

            var xPadding = imageWidth / 30;
            var drawingOptions = new DrawingOptions
            {
                GraphicsOptions = new GraphicsOptions
                {
                    Antialias = true
                },
                TextOptions = new TextOptions
                {
                    WrapTextWidth = imageWidth - xPadding * 2,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                }
            };

            var verticalCenter = (imageHeight - titleFont.Size) / 2;
            var text = $"{post.PageTitle.ToUpper()}{Environment.NewLine}{post.ReadingTimeData.Minutes} min {post.ReadingTimeData.Seconds} sec";
            imageContext.DrawText(drawingOptions, text, titleFont, Color.MediumPurple, new PointF(xPadding + 3, verticalCenter + 3));
            imageContext.DrawText(drawingOptions, text, titleFont, Color.White, new PointF(xPadding, verticalCenter));
        }

        private void AddBrand(IImageProcessingContext imageProcessingContext, int imageWidth, int imageHeight, Post post)
        {
            DrawingOptions drawingOptions = new DrawingOptions
            {
                GraphicsOptions = new GraphicsOptions
                {
                    Antialias = true
                },
                TextOptions = new TextOptions
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right
                }
            };

            var fontSize = imageHeight / 20;
            var xPadding = imageWidth / 30;
            var font = new Font(_fonts, fontSize, FontStyle.Regular);

            var height = fontSize * 2;
            var rectangularPolygon = new RectangularPolygon(0, imageHeight - height, imageWidth, height);
            imageProcessingContext.Fill(Color.ParseHex("#134e5e"), rectangularPolygon);
            imageProcessingContext.DrawText(drawingOptions, post.SiteTitle, font, Color.ParseHex("#c44225"), new PointF(imageWidth - xPadding, imageHeight - fontSize));
        }
    }
}
