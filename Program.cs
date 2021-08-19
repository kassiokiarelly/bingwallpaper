using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace BingWallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileInfo = GetWallpaperPath();
            Console.WriteLine($"Wallpaper will be downloaded to: {fileInfo.FullName}");
            if (fileInfo.Exists)
            {
                Console.WriteLine("File already exists!");
                return;
            }

            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            DownloadWallpaperFromBing(fileInfo);
        }

        static FileInfo GetWallpaperPath()
        {
            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            var userPicturesPath = Path.Combine(userProfilePath, "Pictures", "BingWallpaper");
            var filename = $"BingWallpaper-{DateTime.UtcNow:yyyy-MM-dd}.jpg";
            var outputFilePath = Path.Combine(userPicturesPath, filename);
            var info = new FileInfo(outputFilePath);

            return info;
        }

        static void DownloadWallpaperFromBing(FileInfo fileInfo)
        {

            var baseUri = new Uri(@"https://bing.com");
            var web = new HtmlWeb();
            var doc = web.Load(baseUri);
            var node = doc.DocumentNode.SelectNodes("//a");
            var downloadNode = node.FirstOrDefault(n => n.GetAttributes().Any(a => a.Name == "download" && a.Value == "BingWallpaper.jpg"));
            var urlDownload = downloadNode.GetAttributeValue("href", string.Empty);
            var imageUri = new Uri(baseUri, urlDownload);

            using (var webClient = new WebClient())
            {
                var buffer = webClient.DownloadData(imageUri);
                if (buffer != null && buffer.Length > 0)
                {
                    using (var fileStream = File.OpenWrite(fileInfo.FullName))
                    {
                        fileStream.Write(buffer, 0, buffer.Length);
                    }
                    Console.WriteLine($"{buffer.Length} bytes downloaded.");
                }
            }
        }
    }
}
