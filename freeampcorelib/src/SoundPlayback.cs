using System;
using NAudio.Wave;

namespace freeampcorelib
{
    public class SoundPlayback : ISoundPlayback
    {
        internal WaveOut OutputDevice;
        #region Ctors

        public SoundPlayback()
        {
            OutputDevice = null;
        }

        #endregion

        protected virtual void OnStartPlaying(SoundPlaybackEventArgs e)
        {
            StartPlaying?.Invoke(this, e);
        }

        #region Methods

        public void Dispose()
        {
            if (OutputDevice != null 
                && OutputDevice.PlaybackState == PlaybackState.Paused 
                && OutputDevice.PlaybackState == PlaybackState.Playing)
            {
                OutputDevice.Stop();
                OutputDevice.Dispose();
            }
            OutputDevice = null;
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
            throw new NotFiniteNumberException();
        }

        #endregion

        #region Property

        public Track Track { get; set; }
        public float Volume { get; set; }
        public float MasterVolume { get; set; }
        public float VolumePick { get; }
        public int Channels { get; }
        public int Bitrate { get; }
        public long CurrentPosition { get; }
        public long LengthAudioData { get; }

        #endregion

        #region Events

        public event EventHandler<SoundPlaybackEventArgs> StartPlaying;
        public event EventHandler<SoundPlaybackEventArgs> StopPlaying;
        public event EventHandler<SoundPlaybackEventArgs> TrackLoaded;

        #endregion
    }

    public class SoundPlaybackEventArgs : EventArgs
    {
        public SoundPlaybackEventArgs(Track track)
        {
            Track = track;
        }

        public Track Track { get; private set; }
    }
}