using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLAC_Downloader_CLI.Spotify
{
    class SpotifyTrack
    {
        private string trackName;
        private string artistName;

        public string ArtistName
        {
            get { return artistName; }
            set { artistName = value; }
        }

        public string TrackName
        {
            get { return trackName; }
            set { trackName = value; }
        }
    }
}
