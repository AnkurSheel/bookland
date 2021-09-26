using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace Bookland.Integration.Tests
{
    [UsesVerify]
    public class PageRenderTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture _httpServerFixture;

        public PageRenderTests(HttpServerFixture fixture)
        {
            _httpServerFixture = fixture;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public async Task Page_is_rendered(string path)
        {
            var settings = new VerifyTests.VerifySettings();
            settings.UseParameters(path);
            settings.ScrubLines(removeLine: line => line.Contains("ETag"));

            if (_httpServerFixture.Client == null)
            {
                Assert.False(true);
                return;
            }
            var response = await _httpServerFixture.Client.GetAsync(path);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            await Verifier.Verify(response, settings);
        }

        [Fact]
        public async Task File_are_outputted_correctly()
        {
            var outputDirectory = HttpServerFixture.GetOutputDirectory();
            var files = Directory.GetFiles(outputDirectory, "*", SearchOption.AllDirectories);
            await Verifier.Verify(files);
        }

        public static IEnumerable<object[]> GetData()
        {
            var outputDirectory = HttpServerFixture.GetOutputDirectory();
            var files = Directory.GetFiles(outputDirectory, "*.html", SearchOption.AllDirectories).Select(x => x.Substring(outputDirectory.Length).Replace("\\", "/")).ToList();

            return files.Select(x => new object[] { x });
        }
    }
}
