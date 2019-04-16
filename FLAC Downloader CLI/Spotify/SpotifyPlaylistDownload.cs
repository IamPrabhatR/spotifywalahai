using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FLAC_Downloader_CLI.Spotify;

namespace FLAC_Downloader_CLI
{
    class SpotifyPlaylistDownload
    {
        public static async Task Manager(string userId, string playlistId)
        {
            string accessToken = await GetAccessToken();
            string str = accessToken;
            accessToken = null;
            Console.WriteLine("Downloading playlist file");
            string playList = await GetPlayList(str, userId, playlistId);
            string str1 = playList;
            File.WriteAllText("spotifyplaylist.json", str1);
            Console.WriteLine("spotifyplaylist.json created");
        }

        private static async Task<string> GetAccessToken()
        {
            SpotifyToken token = new SpotifyToken();
            string postString = string.Format("grant_type=client_credentials");

            byte[] byteArray = Encoding.UTF8.GetBytes(postString);
            string url = "https://accounts.spotify.com/api/token";

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic N2FmYTU2MjcxZDdkNDI5NGJmNGYyNzUyN2MzYmEzMzY6YWExYTc0MjlkYjU1NDgzZGFkZjFjZDIyNzY4OTdhMzA="); request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            token = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
                        }
                    }
                }
            }
            return token.access_token;
        }

        public static async Task<string> GetPlayList(string token, string userId, string playlistId)
        {
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists/{1}", userId, playlistId);
            string response = await GetSpotifyType(token, url);
            return response;
        }

        private static async Task<string> GetSpotifyType(string token, string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json; charset=utf-8";
                string responseFromServer;

                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            responseFromServer = reader.ReadToEnd();
                        }
                    }
                }
                return responseFromServer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex); throw;
            }
        }
    }
}
