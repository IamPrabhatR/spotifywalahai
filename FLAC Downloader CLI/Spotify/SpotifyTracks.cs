using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLAC_Downloader_CLI.Spotify
{
    class SpotifyTracks
    {
        public class Data
        {
            public List<SpotifyTracks.TrackData> trackData;
        }

        public class TrackData
        {
            public string trackName;
            public string artistName;
        }
    }
}
