using System.Threading.Tasks;
using Statiq.App;
using Statiq.Web;

namespace Bookland
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await Bootstrapper
                .Factory
                .CreateWeb(args)
                .RunAsync();
        }
    }
}
