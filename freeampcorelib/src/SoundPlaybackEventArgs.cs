using System;

namespace freeampcorelib
{
    public class SoundPlaybackEventArgs : EventArgs
    {
        public SoundPlaybackEventArgs(Track track)
        {
            Track = track;
        }

        public Track Track { get; private set; }
    }
}