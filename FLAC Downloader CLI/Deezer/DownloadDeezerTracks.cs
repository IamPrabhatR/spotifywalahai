using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FLAC_Downloader_CLI.Deezer
{
    class DownloadDeezerTracks
    {
        private static readonly string jsonRaw = File.ReadAllText("deezertracks.json");
        private static readonly List<DeezerTrack> DeezerTracks = JsonConvert.DeserializeObject<List<DeezerTrack>>(jsonRaw);

        public static void Download()
        {
            System.IO.Directory.CreateDirectory("Output");
            foreach (DeezerTrack track in DeezerTracks)
            {
                GetRequest("https://free-mp3-download.net/dl.php?i=" + track.TrackId + "&c=CAPTCHA&f=flac", "Output/" + track.TrackName + " - " + string.Join("_", track.ArtistName.Split(Path.GetInvalidFileNameChars())) + ".flac").Wait();
                Console.WriteLine("Output/" + track.TrackName + " - " + string.Join("_", track.ArtistName.Split(Path.GetInvalidFileNameChars()))  + ".flac");
            }
        }

        private static async Task GetRequest(string url, string fileName)
        {
            // Construct HTTP request to get the logo
            HttpWebRequest httpRequest = (HttpWebRequest)
                WebRequest.Create(url);
            httpRequest.Method = WebRequestMethods.Http.Post;

            // Get back the HTTP response for web server
            HttpWebResponse httpResponse
                = (HttpWebResponse)httpRequest.GetResponse();
            Stream httpResponseStream = httpResponse.GetResponseStream();

            // Define buffer and buffer size
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;

            // Read from response and write to file
            FileStream fileStream = File.Create(fileName);
            while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
            } // end while
        }
    }
}
