using System;
using NAudio.Wave;

namespace freeampcorelib
{
    public class SoundPlayback : ISoundPlayback
    {

        #region Methods

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Preview()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Property

        public Track Track { get; set; }
        public float Volume { get; set; }
        public float MasterVolume { get; set; }
        public RepeatMode RepeatMode { get; set; }
        public PlaybackState PlaybackState { get; set; }

        #endregion

        #region Events

        public event EventHandler<SoundPlaybackEventArgs> StartPlaying;
        public event EventHandler<SoundPlaybackEventArgs> StopPlaing;

        #endregion

    }

    public class SoundPlaybackEventArgs : EventArgs
    {
        public string Message { get; set; }
        public Track Track { get; private set; }
        public SoundPlaybackEventArgs(string message, Track track)
        {
            Message = message;
            Track = track;
        }
    }
}