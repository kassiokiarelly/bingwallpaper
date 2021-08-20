using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net;

namespace BingWallpaper.Downloader
{
    public class BingDownloader : IImageDownloader
    {
        public byte[] Download()
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
                    return buffer;
                }
                return null;
            }
        }
    }
}
