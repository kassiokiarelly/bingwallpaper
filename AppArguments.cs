using CommandLineParser.Arguments;

namespace BingWallpaper
{
    public class AppArguments
    {
        [ValueArgument(typeof(string), 'o', Description = "Set output directory for downloaded file")]
        public string OutputDir { get; set; }

        [SwitchArgument('v', false, Description = "Show program version")]
        public bool Version { get; set; }

        [SwitchArgument('h', false, Description = "Show program usage")]
        public bool Help { get; set; }
    }
}
