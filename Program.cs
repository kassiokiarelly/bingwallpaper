using BingWallpaper.Downloader;
using System;

namespace BingWallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CommandLineParser.CommandLineParser();
            var appArguments = new AppArguments();
            parser.ShowUsageHeader = "Here is how you use the app: ";
            parser.ShowUsageFooter = "Have fun!";
            parser.ExtractArgumentAttributes(appArguments);
            parser.ParseCommandLine(args);

            if (appArguments.Help)
            {
                parser.ShowUsage();
            }
            else if (appArguments.Version)
            {
                ShowVersion();
            }
            else
            {
                var bingDownloader = new BingDownloader();
                var app = new WallpaperDownloader(appArguments, bingDownloader);
                app.Start();
            }

        }

        private static void ShowVersion()
        {
            var version = typeof(Program).Assembly.GetName().Version;
            Console.WriteLine(version);
        }
    }
}
