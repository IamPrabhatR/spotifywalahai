using FLAC_Downloader_CLI.Deezer;
using FLAC_Downloader_CLI.Spotify;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FLAC_Downloader_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get spotify playlist details
            Console.WriteLine("Public playlist URL?");
            string playlistUrl = Console.ReadLine();
            string playlistId = ParsePlaylistUrl.GetPlaylistId(playlistUrl);
            string userId = ParsePlaylistUrl.GetUserId(playlistUrl);

            // Download spotify playlist json
            SpotifyPlaylistDownload.Manager(userId, playlistId).Wait();

            // Convert to deezer playlist json
            Console.WriteLine("Starting conversion to Deezer");
            ConvertTracks.GetTracks().Wait();

            // Download songs
            Console.WriteLine("Starting track download from Deezer");
            DownloadDeezerTracks.Download();
            Console.WriteLine("Finished!");
        }


    }
}
