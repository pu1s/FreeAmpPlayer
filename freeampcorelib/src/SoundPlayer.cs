using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace freeampcorelib
{
    public class SoundPlayer 
    {
        private List<MMDevice> devices;

        public SoundPlayer()
        {
            devices = new List<MMDevice>();
            CoreAudio.GetMMDeviceCollection(out devices);
        }

        public BlockAlignReductionStream bstream { get; set; }
        public Mp3FileReader FileReader { get; set; }
        public WaveOut DeviceWaveOut { get; set; }

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

        public float PicVolume => GetPicVolume(ref devices);


        public event EventHandler StartPlaying;
        public event EventHandler StopPlaying;
        public event EventHandler TrackLoaded;

        private float GetPicVolume(ref List<MMDevice> devices)
        {
            if (devices == null)
                return 0;
            var picvolumes = new float[devices.Count];
            for (var i = 0; i < picvolumes.Length; i++)
            {
                picvolumes[i] = devices[i].AudioMeterInformation.MasterPeakValue;
            }
            var maxpicvolume = new float();
            foreach (var picvolume in picvolumes)
            {
                if (picvolume > maxpicvolume)
                {
                    maxpicvolume = picvolume;
                }
            }
            Debug.WriteLine("maxPic  {0}", maxpicvolume);
            return maxpicvolume;
        }

        public void Out_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Stop();
        }

        public void Pause()
        {
            if (DeviceWaveOut == null || FileReader == null || DeviceWaveOut.PlaybackState != PlaybackState.Playing)
                return;
            DeviceWaveOut.Pause();
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
        }

        public void DisposeDevice()
        {
            if (DeviceWaveOut != null)
            {
                if (DeviceWaveOut.PlaybackState == PlaybackState.Playing ||
                    DeviceWaveOut.PlaybackState == PlaybackState.Paused)
                {
                    DeviceWaveOut.Stop();
                }
                DeviceWaveOut.PlaybackStopped -= Out_PlaybackStopped;
                DeviceWaveOut = null;
            }
            if (bstream == null) return;
            bstream.Dispose();
            bstream = null;
        }

        public SoundPlayer Load(Track track)
        {
            if (track == null) return null;
            if (DeviceWaveOut == null)
            {
                DeviceWaveOut = new WaveOut();
                DeviceWaveOut.PlaybackStopped += Out_PlaybackStopped;
            }
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

    public interface ISoundPlayback : IDisposable
    {
        void Play();
        void Pause();
        void Stop();
        void Next();
        void Preview();
        float Volume { get; set; }
        float MasterVolume { get; set; }
        RepeatMode RepeatMode { get; set; }
        PlaybackState PlaybackState { get; set; }
        event EventHandler StartPlaying;
        event EventHandler StopPlaing;
        Track Track { get; set; }
    }
}