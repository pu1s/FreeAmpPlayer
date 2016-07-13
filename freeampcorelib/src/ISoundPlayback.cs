using System;
using NAudio.Wave;

namespace freeampcorelib
{
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
        event EventHandler<SoundPlaybackEventArgs> StartPlaying;
        event EventHandler<SoundPlaybackEventArgs> StopPlaing;
        Track Track { get; set; }
    }
}