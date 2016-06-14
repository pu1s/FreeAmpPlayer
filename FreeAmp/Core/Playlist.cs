using System.Collections.Generic;

namespace FreeAmp.Core
{
    public class TrackList
    {
        private uint curPos;

        static TrackList()
        {
            EmptyTrackList();
        }

        public string Name { get; set; }
        public List<Track> Tracks { get; }
        public int Count => Tracks.Count;

        public uint CurPos
        {
            get { return Tracks.Count == 0 ? 0 : curPos; }
            set { curPos = value; }
        }

        public Track this[int index] => Tracks[index];

        private static TrackList EmptyTrackList()
        {
            return new TrackList();
        }
    }
}