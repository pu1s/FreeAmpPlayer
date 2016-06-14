using System;

namespace FreeAmp.Core
{
    public class SoundPlayer : IPlayer
    {
        #region Main Func

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            TrackList.CurPos = (uint)rnd.Next(0, TrackList.Count);
        }
    }
}