using System;
using NAudio.Wave;

namespace FreeAmp.Core
{
    public class SoundPlayer : IPlayer
    {
        #region Main Func

        private NAudio.Wave.IWavePlayer OutDevice { get; set; }

        public SoundPlayer SetOutDevice(IWavePlayer outDevice)
        {
            OutDevice = outDevice;
            return this;
        }
        
        public void Play()
        { 
            AudioFileReader reader;
           reader =
               new AudioFileReader(TrackList.GetCurrentTrack().Name);
            OutDevice.Init(reader);
            OutDevice.Play();
            IsPlaying = true;
        }

        public void Pause()
        {
            OutDevice.Pause();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
           TrackList.NextTrack();
        }

        public void Preview()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Events

        public event EventHandler Playing;
        public event EventHandler Paused;
        public event EventHandler Stoped;

        #endregion

        #region Property

        public bool IsSuttle { get; set; }

        public TrackList TrackList { get; set; }

        /// <summary>
        /// Возвращает и задает алгоритм повтора треков
        /// </summary>
        public RepeatMode IsRepeat { get; set; }

        public bool IsPlaying { get; set; }

        #endregion

        /// <summary>
        /// Вычисляет псевдослучайное число
        /// </summary>
        private void Shuttle()
        {
            if (TrackList.IsEmpty) return;
            var rnd = new Random(TrackList.Count);
            TrackList.CurPos = (uint) rnd.Next(0, TrackList.Count);
        }
    }

    public enum AudioFileFormat
    {
        wav,
        mp3,
        mp2,
        mp4
    };
}