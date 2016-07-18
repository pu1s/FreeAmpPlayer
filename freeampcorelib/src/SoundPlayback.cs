using System;
using System.IO;
using NAudio.Wave;

namespace freeampcorelib
{
    public class SoundPlayback : ISoundPlayback
    {
        internal WaveOut OutputDevice;
        internal WaveFormat OutputWaveFormat;
        internal Mp3FileReader FileReader;
        #region Ctors

        public SoundPlayback()
        {
            OutputDevice = null;
            OutputWaveFormat = null;
            FileReader = null;
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
            FileReader = new Mp3FileReader(Track.Path);
            var pcm = WaveFormatConversionStream.CreatePcmStream(FileReader);
            OutputWaveFormat = pcm.WaveFormat;
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
        public float Volume
        {
            get
            {
                return OutputDevice?.Volume ?? 0;
            }
            set
            {
                if(OutputDevice == null) return;
                if(value>1 && value<0) throw new ArgumentOutOfRangeException(nameof(value));
                OutputDevice.Volume = value;
            }
        }
        public float MasterVolume { get; set; }

        public float VolumePick
        {
            get
            {
                if (OutputDevice == null)
                {
                    return 0;
                }
                return OutputDevice.Volume;
            }
        }

        public int Channels => FileReader.WaveFormat?.Channels ?? 0;

        public int Bitrate => FileReader?.WaveFormat.SampleRate ?? 0;

        public long CurrentPosition => OutputDevice?.GetPosition() ?? 0;

        public long LengthAudioData => FileReader?.Length ?? 0;

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