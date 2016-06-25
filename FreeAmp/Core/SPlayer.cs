using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using NAudio.Wave;

namespace FreeAmp.Core
{
    public class SPlayer
    {
        public SPlayer()
        {
            _device = new WaveOut();
            _audioFileReader = null;
            TrackList = TrackList.Empty;
            Slider = new Slider();
        }


       public Slider Slider { get; set; }
        
        public TrackList TrackList { get; set; }
        public bool IsPlaying => _device?.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => _device?.PlaybackState == PlaybackState.Paused;
        public bool IsStopped => _device?.PlaybackState == PlaybackState.Stopped;
        private WaveOut _device;
        private AudioFileReader _audioFileReader;

        public AudioFileReader AudioFileReader
        {
            get { return _audioFileReader; }
        }


        public WaveOut Device => _device;

        public double TrackTotalTime
        {
            get
            {
                Slider.Maximum = _audioFileReader.TotalTime.TotalSeconds;
                return _audioFileReader.TotalTime.TotalSeconds;
            }
        }

        public event PropertyChangedEventHandler PosChanged;
        public double TrackCurrentPosition
        {
            get
            {
                Slider.Value = _audioFileReader.CurrentTime.TotalSeconds;
                return _audioFileReader.CurrentTime.TotalSeconds;
            }
            set
            {
                if (value < 0 || value > TrackTotalTime)
                    return;
                _audioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
            }
        }

        public event EventHandler PlayStart;
        public event EventHandler PlayStop;
        public event EventHandler PlayResume;
        public event EventHandler PlayPause;

        public void Play()
        {
            if (_audioFileReader != null)
            {
                _audioFileReader = null;
            }
            _audioFileReader = new AudioFileReader(TrackList.GetCurrentTrack().TrackInfo.FilePath);
            if (IsPlaying)
            {
                _device.Stop();
                _device = new NAudio.Wave.WaveOut();
            }

            if (IsStopped)
            {
                _device = new WaveOut();
            }
            _device.Init(_audioFileReader);
            _device.Play();

            OnPlayStarted();
        }

        public void Play(Track track)
        {
            _audioFileReader = new AudioFileReader(track.Path);
            _device = new WaveOut();
            _device.Init(_audioFileReader);
            _device.Play();
            OnPlayStarted();
        }

        public void Stop()
        {
            if (_device == null || _device.PlaybackState == PlaybackState.Stopped) return;
            if (_audioFileReader == null) return;
            _device.Stop();
            _audioFileReader = null;
            OnPlayStopped();
        }

        public void Resume()
        {
            if (_device == null || _device.PlaybackState == PlaybackState.Playing) return;
            if (_audioFileReader == null) return;
            _device.Resume();
            OnPlayResume();
        }

        public void Pause()
        {
            if (_device == null || _device.PlaybackState == PlaybackState.Paused) return;
            if (_audioFileReader == null) return;
            _device.Pause();
            OnPlayPause();
        }

        public void Next()
        {
            try
            {
                if (_device.PlaybackState == PlaybackState.Playing)
                {
                    _device.Stop();
                }
                _device = null;
                _audioFileReader = null;
                TrackList.NextTrack();
                Play(TrackList.GetCurrentTrack());
            }
            catch (TrackListException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Prewiev()
        {
            try
            {
                if (_device.PlaybackState == PlaybackState.Playing)
                {
                    _device.Stop();
                }
                _device = null;
                _audioFileReader = null;
                TrackList.PreviewTrack();
                Play(TrackList.GetCurrentTrack());
            }
            catch (TrackListException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected virtual void OnPlayStarted()
        {
            PlayStart?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPlayStopped()
        {
            PlayStop?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPlayResume()
        {
            PlayResume?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPlayPause()
        {
            PlayPause?.Invoke(this, EventArgs.Empty);
        }


        protected virtual void OnPosChanged(PropertyChangedEventArgs e)
        {
            PosChanged?.Invoke(this, e);OnPosChanged(new PropertyChangedEventArgs("Position"));
        }
    }
}