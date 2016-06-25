using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        public TrackList TrackList { get; set; }
        public bool IsPlaying => _device?.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => _device?.PlaybackState == PlaybackState.Paused;
        public bool IsStoped => _device?.PlaybackState == PlaybackState.Stopped;
        private WaveOut _device;
        private AudioFileReader _audioFileReader;
        public WaveOut Device => _device;
        public long CurrentTrackLength => _audioFileReader?.Length ?? 0;
        public long CurrentTrackPosition => _audioFileReader?.Position ?? 0;
        public event EventHandler PlayStart;
        public event EventHandler PlayStop;
        public event EventHandler PlayResume;
        public event EventHandler PlayPause;

        public void Play(Track track)
        {
            _audioFileReader = new AudioFileReader(track.Name);
            _device = new WaveOut();
            _device.Init(_audioFileReader);
            _device.Play();
            OnPlayStarted();
        }

        public void Stop()
        {
            if(_device == null || _device.PlaybackState == PlaybackState.Stopped) return;
            if(_audioFileReader == null) return;
            _device.Stop();
            _audioFileReader = null;
            OnPlayStoped();
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
            if (_device.PlaybackState == PlaybackState.Playing)
            {
                _device.Stop();
            }
            _device = null;
            _audioFileReader = null;
            TrackList.NextTrack();
            Play(TrackList.GetCurrentTrack());

        }
        protected virtual void OnPlayStarted()
        {
            PlayStart?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPlayStoped()
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
    }
}
