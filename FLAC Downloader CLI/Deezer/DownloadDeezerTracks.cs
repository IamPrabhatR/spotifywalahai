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
            WebClient wc = new WebClient();
            System.IO.Directory.CreateDirectory("Output");

            foreach (DeezerTrack track in DeezerTracks)
            {
                wc.DownloadFile("https://free-mp3-download.net/dl.php?i="+ track.TrackId + "&c=CAPTCHA&f=flac", "Output/" + track.TrackName + " - " + track.ArtistName + ".flac");
            }
        }
    }
}
