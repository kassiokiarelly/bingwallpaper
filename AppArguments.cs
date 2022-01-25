using CommandLine;

namespace BingWallpaper
{
    public class AppArguments
    {
        [Option('o', "OutputDir", HelpText = "Set output directory for downloaded file")]
        public string OutputDir { get; set; }
        
        
        [Option('u', "UpdateWallpaper", HelpText = "Update desktop wallpaper after download")]
        public bool UpdateWallpaper { get; set; }
    }
}
