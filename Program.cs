using BingWallpaper.Downloader;
using System;
using BingWallpaper.SystemInterop;
using CommandLine;

namespace BingWallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AppArguments>(args)
                .WithParsed(Run);
        }

        private static void Run(AppArguments appArguments)
        {
            var bingDownloader = new BingDownloader();
            var systemInterop = new WindowsInterop();
            var app = new Application(appArguments, bingDownloader, systemInterop);
            app.Start();
        }
    }
}