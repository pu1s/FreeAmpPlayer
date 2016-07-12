using System;
using System.Collections.Generic;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace freeampcorelib
{
   

    public class SoundPlayer
    {
        public SoundPlayer()
        {
           
            devices = new List<MMDevice>();
            CoreAudio.GetMMDeviceCollection(out devices);
            DeviceWaveOut = new DirectSoundOut();
            DeviceWaveOut.PlaybackStopped += Out_PlaybackStopped;
           
        }

        
        private List<MMDevice> devices;
       
        public BlockAlignReductionStream bstream { get; set; }
        public Mp3FileReader FileReader { get; set; }
        public DirectSoundOut DeviceWaveOut { get; set; }

        public PlaybackState PlaybackState => DeviceWaveOut.PlaybackState;
        public double fileVol => DeviceWaveOut.Volume;
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

        public float PicVolume => devices[1].AudioMeterInformation.MasterPeakValue;

        public event EventHandler StartPlaying;
        public event EventHandler StopPlaying;
        public event EventHandler TrackLoaded;

        public void Out_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Stop();
        }

        public void Pause()
        {
            if (DeviceWaveOut == null || FileReader == null || DeviceWaveOut.PlaybackState != PlaybackState.Playing)
                return;
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
            if (DeviceWaveOut == null || FileReader == null || DeviceWaveOut.PlaybackState != PlaybackState.Paused)
                return;
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

        public void DisposeDevice()
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

            var pcm = WaveFormatConversionStream.CreatePcmStream(FileReader);
            bstream = new BlockAlignReductionStream(pcm);
          
            DeviceWaveOut.Init(bstream);
            OnTrackLoaded();


            return this;
        }

        public virtual void OnStartPlaying()
        {
            StartPlaying?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnStopPlaying()
        {
            StopPlaying?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnTrackLoaded()
        {
            TrackLoaded?.Invoke(this, EventArgs.Empty);
        }
    }
}