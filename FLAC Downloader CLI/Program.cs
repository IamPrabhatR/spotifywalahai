using FLAC_Downloader_CLI.Deezer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FLAC_Downloader_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Public playlist URL?");
            string playlistUrl = Console.ReadLine();
            string playlistId = ParsePlaylistUrl.GetPlaylistId(playlistUrl);
            string userId = ParsePlaylistUrl.GetUserId(playlistUrl);
            Console.WriteLine(string.Concat("Your spotify playlist id is ", playlistId));
            Console.WriteLine(string.Concat("Your user id is ", userId));
            DownloadSpotifyPlaylistJson(userId, playlistId).Wait();
            Console.WriteLine("Starting conversion to Deezer");
            ConvertToDeezer();
        }

        private static void ConvertToDeezer()
        {
            ConvertTracks.GetTracks().Wait();
        }

        private static async Task DownloadSpotifyPlaylistJson(string userId, string playlistId)
        {
            string accessToken = await SpotifyPlaylistDownload.GetAccessToken();
            string str = accessToken;
            accessToken = null;
            Console.WriteLine("Downloading playlist file");
            string playList = await SpotifyPlaylistDownload.GetPlayList(str, userId, playlistId);
            string str1 = playList;
            playList = null;
            File.WriteAllText("playlist.json", str1);
            Console.WriteLine("playlist.json created");
        }
    }
}
