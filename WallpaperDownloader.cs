using BingWallpaper.Downloader;
using System;
using System.IO;

namespace BingWallpaper
{
    public class WallpaperDownloader
    {
        private readonly AppArguments args;
        private readonly IImageDownloader downloader;

        public WallpaperDownloader(AppArguments args, IImageDownloader downloader)
        {
            this.args = args;
            this.downloader = downloader;
        }

        public void Start()
        {
            var fileInfo = GetDefaultPath();
            Console.WriteLine($"Wallpaper will be downloaded to: {fileInfo.FullName}");
            if (fileInfo.Exists)
            {
                Console.WriteLine("File already exists!");
                return;
            }

            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            var buffer = downloader.Download();
            using (var file = File.OpenWrite(fileInfo.FullName))
                file.Write(buffer, 0, buffer.Length);
        }


        private FileInfo GetDefaultPath()
        {
            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            var userPicturesPath = Path.Combine(userProfilePath, "Pictures", "BingWallpaper");
            var filename = GetFilename();
            var outputFilePath = Path.Combine(userPicturesPath, filename);
            var info = new FileInfo(outputFilePath);

            return info;
        }

        private string GetFilename() => $"BingWallpaper-{DateTime.UtcNow:yyyy-MM-dd}.jpg";
    }
}
