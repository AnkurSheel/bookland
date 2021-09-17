using System.IO;
using System.Threading.Tasks;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Bookland.Services
{
    public class ImageService : IImageService
    {
        private readonly FontFamily _cookieFont;

        public ImageService()
        {
            _cookieFont = new FontCollection().Install("./input/assets/fonts/Cookie-Regular.ttf");
        }

        public async Task<Stream> CreateImageDocument(int width, int height, string coverImagePath, string siteTitle, string centerText)
        {
            using Image template = new Image<Rgb24>(width, height);
            using Image thumbnail = await Image.LoadAsync(coverImagePath);

            thumbnail.Mutate(
                imageContext =>
                {
                    imageContext.SetGraphicsOptions(
                        new GraphicsOptions
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
                    AddCenterText(imageContext, width, height, centerText);
                    AddBrand(imageContext, width, height, siteTitle);
                });

            Stream output = new MemoryStream();

            await template.SaveAsPngAsync(output);

            return output;
        }

        private void AddGradient(int width, int height, IImageProcessingContext imageContext)
        {
            imageContext.Fill(
                new LinearGradientBrush(
                    new PointF(0, 0),
                    new PointF(width / 2, height / 2),
                    GradientRepetitionMode.Reflect,
                    new ColorStop(0f, Color.ParseHex("#16222A")),
                    new ColorStop(0.5f, Color.ParseHex("#3A6073"))));
        }

        private void DarkenImage(IImageProcessingContext imageContext)
        {
            imageContext.Lightness(0.5f);
        }

        private void ResizeImage(int width, int height, IImageProcessingContext imageContext)
        {
            imageContext.Resize(
                new ResizeOptions
                {
                    Position = AnchorPositionMode.Center,
                    Size = new Size(width, height),
                    Mode = ResizeMode.Pad,
                });
        }

        private void AddCenterText(IImageProcessingContext imageContext, int imageWidth, int imageHeight, string centerText)
        {
            var fontSize = imageHeight / 10;
            var titleFont = new Font(_cookieFont, fontSize, FontStyle.Bold);

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
            imageContext.DrawText(drawingOptions, centerText, titleFont, Color.MediumPurple, new PointF(xPadding + 2, verticalCenter + 2));
            imageContext.DrawText(drawingOptions, centerText, titleFont, Color.White, new PointF(xPadding, verticalCenter));
        }

        private void AddBrand(IImageProcessingContext imageProcessingContext, int imageWidth, int imageHeight, string siteTitle)
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
            var font = new Font(_cookieFont, fontSize, FontStyle.Regular);

            var height = fontSize * 2;
            var rectangularPolygon = new RectangularPolygon(0, imageHeight - height, imageWidth, height);
            imageProcessingContext.Fill(Color.ParseHex("#134e5e"), rectangularPolygon);
            imageProcessingContext.DrawText(drawingOptions, siteTitle, font, Color.ParseHex("#c44225"), new PointF(imageWidth - xPadding, imageHeight - fontSize));
        }
    }
}
