using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace FreeAmp.Core
{
    public class SoundPlayer
    {
       

        public SoundPlayer()
        {
            DeviceWaveOut = new WaveOut();
            DeviceWaveOut.PlaybackStopped += Out_PlaybackStopped;
        }
        private void Out_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Stop();
        }

        private Mp3FileReader FileReader { get; set; } = null;
        private WaveOut DeviceWaveOut { get; set; } = null;

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

        public void Play()
        {
            
            DeviceWaveOut.Play();
        }

        public void Pause()
        {
            if (DeviceWaveOut == null || FileReader == null || DeviceWaveOut.PlaybackState != PlaybackState.Playing) return;
            DeviceWaveOut.Pause();
        }

        public void Resume()
        {
            if (DeviceWaveOut == null || FileReader == null || DeviceWaveOut.PlaybackState != PlaybackState.Paused) return;
            DeviceWaveOut.Play();
        }

        public void Stop()
        {
            if (DeviceWaveOut == null || FileReader == null) return;
            DeviceWaveOut.Stop();
            DeviceWaveOut.Dispose();
            FileReader.Dispose();
        }

        public SoundPlayer Load(TrackList trackList)
        {
            if (trackList == null) return null;
           
            DeviceWaveOut = new WaveOut();
            FileReader = new Mp3FileReader(trackList?.GetCurrentTrack().Path);
            DeviceWaveOut.Init(FileReader);
            return this;
        }
    }
}