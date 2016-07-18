using System;
using System.IO;
using NAudio.Wave;

namespace freeampcorelib
{
    public class SoundPlayback : ISoundPlayback
    {
        internal WaveOut OutputDevice;
        internal WaveFormat OutputWaveFormat;
        #region Ctors

        public SoundPlayback()
        {
            OutputDevice = null;
            OutputWaveFormat = null;
        }

        #endregion

        protected virtual void OnStartPlaying(SoundPlaybackEventArgs e)
        {
            StartPlaying?.Invoke(this, e);
        }

        protected virtual void OnTrackLoaded(SoundPlaybackEventArgs e)
        {
            TrackLoaded?.Invoke(this, e);
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

        /// <summary>
        /// </summary>
        public void Play()
        {
            if (Track == null) throw new SoundPlaybackException(nameof(Track));

            if (!File.Exists(Track.Path)) throw new FileNotFoundException(nameof(Track.Path));
            OutputDevice = new WaveOut();
            
            //TODO: определить фильтр файлов по расширению

            var fileStream = new Mp3FileReader(Track.Path);
            LengthAudioData = fileStream.Length;
            Channels = fileStream.WaveFormat.Channels;
            Bitrate = fileStream.WaveFormat.AverageBytesPerSecond;
            CurrentPosition = fileStream.Position;
            var pcm = WaveFormatConversionStream.CreatePcmStream(fileStream);
            //TODO: +++++++++++++++++++++++++++++++++++++++
            OutputWaveFormat = pcm;
            OutputDevice.Init(pcm);
            OnTrackLoaded(new SoundPlaybackEventArgs(Track));
            OutputDevice.Play();
            OnStartPlaying(new SoundPlaybackEventArgs(Track));
            OutputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
        }

        private void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
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
        public int Channels { get; private set; }
        public int Bitrate { get; private set; }
        public long CurrentPosition { get; private set; }
        public long LengthAudioData { get; private set; }

        #endregion

        #region Events

        public event EventHandler<SoundPlaybackEventArgs> StartPlaying;
        public event EventHandler<SoundPlaybackEventArgs> StopPlaying;
        public event EventHandler<SoundPlaybackEventArgs> TrackLoaded;

        #endregion
    }

    public class SoundPlaybackException : Exception
    {
        public SoundPlaybackException()
        {
        }

        public SoundPlaybackException(string message) : base(message)
        {
            Message = message;
        }

        public new string Message { get; private set; }
    }
}