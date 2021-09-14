using System.IO;
using System.Threading.Tasks;
using Bookland.Models;
using Statiq.Common;

namespace Bookland.Services
{
    public interface IImageService
    {
        Task<Stream> CreateImageDocument(
            int width,
            int height,
            string coverImagePath,
            string pageTitle,
            ReadingTimeData readingTimeData,
            string siteTitle);
    }
}
