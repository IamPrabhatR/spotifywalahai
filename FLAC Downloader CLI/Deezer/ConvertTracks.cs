using E.Deezer;
using E.Deezer.Api;
using FLAC_Downloader_CLI.Spotify;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FLAC_Downloader_CLI.Deezer
{
    class ConvertTracks
    {
        private static readonly string jsonRaw = File.ReadAllText("spotifyplaylist.json");
        private static readonly List<DeezerTrack> DeezerTracks = new List<DeezerTrack>();
        private static readonly SpotifyPlaylist.Rootobject SpotifyTracks = JsonConvert.DeserializeObject<SpotifyPlaylist.Rootobject>(jsonRaw);
        private static readonly int totalTracks = SpotifyTracks.tracks.items.Count();
        private static int totalTracksChecked = 0;
        private static int totalTracksFound = 0;

        public static async Task GetTracks()
        {
            var deezer = DeezerSession.CreateNew();
            for (int i = 0; i < SpotifyTracks.tracks.items.Count(); i++)
            {
                IEnumerable<ITrack> tracks = await deezer.Search.Tracks(SpotifyTracks.tracks.items[i].track.name, 0, 100);
                IEnumerable<ITrack> tracks1 = tracks;
                ITrack track = FindCorrectTrack(tracks1, i);
                if (track != null)
                {
                    totalTracksChecked++;
                    totalTracksFound++;
                    //Console.WriteLine("Track found on Deezer!");
                    Utilities.Utilities.DrawTextProgressBar(totalTracksChecked, totalTracks);
                    SerializeTrack(track.Id.ToString(), track.Title, track.ArtistName);
                }
                else
                {
                    totalTracksChecked++;
                    Utilities.Utilities.DrawTextProgressBar(totalTracksChecked, totalTracks);
                    //Console.WriteLine("TRACK NOT FOUND ON DEEZER");
                }
            }
            File.WriteAllText("deezertracks.json", JsonConvert.SerializeObject(DeezerTracks));
            Console.WriteLine(DeezerTracks.Count);
            Console.WriteLine("Conversion to Deezer json file complete. {0} tracks found on Deezer out of {1} tracks.", totalTracksFound, totalTracks);
        }

        private static void SerializeTrack(string trackId, string trackName, string artistName)
        {
            DeezerTrack Track = new DeezerTrack()
            {
                TrackId = trackId,
                TrackName = trackName,
                ArtistName = artistName
            };
            DeezerTracks.Add(Track);
        }

        private static ITrack FindCorrectTrack(IEnumerable<ITrack> tracks, int index)
        {
            ITrack track;
            foreach (ITrack DeezerTrack in tracks)
            {
                if ((DeezerTrack.Title != SpotifyTracks.tracks.items[index].track.name ? false : DeezerTrack.ArtistName == SpotifyTracks.tracks.items[index].track.album.artists[0].name))
                {
                    track = DeezerTrack;
                    return track;
                }
            }
            track = null;
            return track;
        }
    }
}
