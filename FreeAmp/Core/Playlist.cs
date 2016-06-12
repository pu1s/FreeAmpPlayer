using System.Collections.Generic;
using System.Dynamic;
using System.Windows;

namespace FreeAmp.Core
{
    public class TrackList
    {
        static TrackList()
        {
            EmptyTrackList();
        }

        public string Name { get; set; }
        public List<Track> Tracks { get; }
        public int Count => Tracks.Count;
        public Track this[int index] => Tracks[index];
        private static TrackList EmptyTrackList()
        {
            return new TrackList();
        }
    }
}