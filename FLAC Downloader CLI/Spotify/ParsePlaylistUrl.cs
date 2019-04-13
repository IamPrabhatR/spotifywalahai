using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLAC_Downloader_CLI.Deezer
{
    class ParsePlaylistUrl
    {
        public static string GetPlaylistId(string url)
        {
            string result = url.Substring(url.LastIndexOf("playlist/") + 9);
            result = result.Substring(0, result.IndexOf("?"));
            return result;
        }

        public static string GetUserId(string url)
        {
            string result = url.Substring(url.LastIndexOf("user/") + 5);
            result = result.Substring(0, result.IndexOf("/playlist"));
            return result;
        }
    }
}
