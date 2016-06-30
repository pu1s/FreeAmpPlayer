using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using NAudio.Gui;
using NAudio.Wave;

namespace FreeAmp.Core
{
    public class SoundPlayer
    {
       

        public SoundPlayer()
        {
            DeviceWaveOut = new DirectSoundOut();
            DeviceWaveOut.PlaybackStopped += Out_PlaybackStopped;
        }

        public event EventHandler StartPlaying;
        public event EventHandler StopPlaying;
        public event EventHandler TrackLoaded;
        private void Out_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Stop();
        }

        private BlockAlignReductionStream bstream { get; set; } = null;
        private Mp3FileReader FileReader { get; set; } = null;
        private DirectSoundOut DeviceWaveOut { get; set; } = null;

        public PlaybackState PlaybackState => DeviceWaveOut.PlaybackState;

        public double TrackTotalTime => FileReader.TotalTime.TotalSeconds;

        public double CurrentTrackTime
        {
            get { return FileReader.CurrentTime.TotalSeconds; }
            set
            {
                if (!(value < 0) && !(value > TrackTotalTime))
                {
                    FileReader.CurrentTime = TimeSpan.FromSeconds(value);
                }
            }
        }

        public int BitRate => FileReader.Mp3WaveFormat.BitsPerSample;

        public int Channels => FileReader.Mp3WaveFormat.Channels;

        public void Pause()
        {
            if (DeviceWaveOut == null || FileReader == null || DeviceWaveOut.PlaybackState != PlaybackState.Playing) return;
            DeviceWaveOut.Pause();
            OnStopPlaying();
        }

        public void Play()
        {
           
            DeviceWaveOut.Play();
            OnStartPlaying();
        }
        public void Resume()
        {
            if (DeviceWaveOut == null || FileReader == null || DeviceWaveOut.PlaybackState != PlaybackState.Paused) return;
            DeviceWaveOut.Play();
            OnStartPlaying();
        }

        public void Stop()
        {
            if (DeviceWaveOut == null || FileReader == null) return;
            DeviceWaveOut.Stop();
            OnStopPlaying();
           DisposeDevice();
           
        }

        private void DisposeDevice()
        {
            if (DeviceWaveOut != null)
            {
                if (DeviceWaveOut.PlaybackState == PlaybackState.Playing ||
                    DeviceWaveOut.PlaybackState == PlaybackState.Paused)
                {
                    DeviceWaveOut.Stop();
                    DeviceWaveOut = null;
                }
            }
            if (bstream == null) return;
            bstream.Dispose();
            bstream = null;
        }
        public SoundPlayer Load(Track track)
        {
            DisposeDevice();
            if (track == null) return null;
            
            DeviceWaveOut = new DirectSoundOut();
            FileReader = new Mp3FileReader(track.Path);
           
            WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(FileReader);
            bstream = new BlockAlignReductionStream(pcm);
            DeviceWaveOut.Init(bstream);
            OnTrackLoaded();
            
            return this;
            
        }

        protected virtual void OnStartPlaying()
        {
            StartPlaying?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnStopPlaying()
        {
            StopPlaying?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnTrackLoaded()
        {
            TrackLoaded?.Invoke(this, EventArgs.Empty);
        }
    }


}