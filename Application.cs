using BingWallpaper.Downloader;
using System;
using System.IO;
using BingWallpaper.SystemInterop;

namespace BingWallpaper
{
    public class Application
    {
        private readonly AppArguments _args;
        private readonly IImageDownloader _downloader;
        private readonly ISystemInterop _systemInterop;

        public Application(AppArguments args, IImageDownloader downloader, ISystemInterop systemInterop)
        {
            _args = args;
            _downloader = downloader;
            _systemInterop = systemInterop;
        }

        public void Start()
        {
            var fileInfo = GetFilePath();
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
            
            Console.WriteLine($"Wallpaper will be downloaded to: {fileInfo.FullName}");
            if (fileInfo.Exists)
            {
                Console.WriteLine("File already exists!");
            }
            else
            {
                var buffer = _downloader.Download();
                using (var file = File.OpenWrite(fileInfo.FullName))
                    file.Write(buffer, 0, buffer.Length);
            }

            if (_args.UpdateWallpaper)
                _systemInterop.SetWallpaper(fileInfo.FullName);
        }

        private string GetFileDirectory()
        {
            if (string.IsNullOrWhiteSpace(_args.OutputDir))
            {
                var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
                var userPicturesPath = Path.Combine(userProfilePath, "Pictures", "BingWallpaper");
                return userPicturesPath;
            }
            return _args.OutputDir;
        }

        private FileInfo GetFilePath()
        {
            var directory = GetFileDirectory();
            var filename = GetFilename();
            var outputFilePath = Path.Combine(directory, filename);
            var info = new FileInfo(outputFilePath);

            return info;
        }

        private string GetFilename() => $"BingWallpaper-{DateTime.UtcNow:yyyy-MM-dd}.jpg";
    }
}
