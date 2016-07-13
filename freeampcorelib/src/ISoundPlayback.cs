using System;
using NAudio.Wave;

namespace freeampcorelib
{
    public interface ISoundPlayback : IDisposable
    {
        void Play();
        void Pause();
        void Stop();
        float Volume { get; set; }
        float MasterVolume { get; set; }
        float VolumePick { get; }
        int Channels { get; }
        int Bitrate { get; }
        long CurrentPosition { get; }
        long LengthAudioData { get; }
        event EventHandler<SoundPlaybackEventArgs> StartPlaying;
        event EventHandler<SoundPlaybackEventArgs> StopPlaying;
        event EventHandler<SoundPlaybackEventArgs> TrackLoaded;
        Track Track { get; set; }
    }
}