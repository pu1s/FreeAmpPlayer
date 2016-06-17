using System;

namespace FreeAmp.Core
{
    public class TrackListLadedEventArgs :EventArgs

    {
        private readonly TrackList _trackList;

        public TrackList TrackList => _trackList;

        public TrackListLadedEventArgs(TrackList trackList)
        {
            _trackList = trackList;
        }
    }
}