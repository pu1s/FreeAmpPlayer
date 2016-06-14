using System;

namespace FreeAmp.Core
{
    public interface IPlayer
    {
        void Play();
        void Pause();
        void Resume();
        void Stop();
        void Next();
        void Preview();
        TrackList TrackList { get; set; }
        event EventHandler Playing;
        event EventHandler Paused;
        event EventHandler Stoped;
        bool IsSuttle { get; set; }
        RepeatMode IsRepeat { get; set; }
        bool IsPlaying { get; set; }
    }
}