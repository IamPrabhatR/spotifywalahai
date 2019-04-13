using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLAC_Downloader_CLI.Deezer
{
    class DeezerTrack
    {
        private string trackId;
        private string trackName;
        private string artistName;

        public string TrackId
        {
            get {  return trackId; }
            set { trackId = value; }
        }

        public string TrackName
        {
            get { return trackName; }
            set { trackName = value; }
        }

        public string ArtistName
        {
            get { return artistName; }
            set { artistName = value; }
        }
    }
}
