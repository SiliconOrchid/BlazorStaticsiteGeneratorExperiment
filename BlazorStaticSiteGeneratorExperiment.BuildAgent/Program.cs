using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace BlazorStaticSiteGeneratorExperiment.BuildAgent
{
    class Program
    {
        private static BuildAgentAppHost _buildAgentAppHost;
        private static HttpClient _httpClient;
        private static string _outputPath;

        static async Task Main(string[] args)
        {
            _buildAgentAppHost = new BuildAgentAppHost();
            _httpClient = _buildAgentAppHost.CreateDefaultClient();
            _outputPath = "c:/tempOutput";

            await Render("/");
            await Render("/fetchdata");
            await Render("/counter");
            await Render("/blog");
            await Render("/blog/awesome");
            await Render("/blog/dummy-title");

        }

        public static async Task Render(string route)
        {
            // strip the leading slash
            var renderPath = route.Substring(1);

            // create the output directory
            var relativePath = Path.Combine(_outputPath, renderPath);
            var outputDirectory = Path.GetFullPath(relativePath);
            Directory.CreateDirectory(outputDirectory);

            // Build the output file path
            var filePath = Path.Combine(outputDirectory, "index.html");

            // Call the prerendering API, and write the contents to the file
            var result = await _httpClient.GetStreamAsync(route);
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await result.CopyToAsync(file);
            }


        }

    }
}
