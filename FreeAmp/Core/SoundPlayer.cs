using System;

namespace FreeAmp.Core
{
    public class SoundPlayer : IPlayer
    {
        #region Fields

        private TrackList _trackList;

        #endregion

        #region Main Func

        /// <summary>
        ///     Начинает проигрывание
        /// </summary>
        public void Play()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Приостанавливает проигрывание
        /// </summary>
        public void Pause()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Возобновляет проигрывание
        /// </summary>
        public void Resume()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Останавливает проигрывание
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Осуществляет переход к следующему треку в плей листе
        /// </summary>
        public void Next()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Осуществляет переход к предидущему треку в плей листе
        /// </summary>
        public void Preview()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Events

        public event EventHandler Playing;
        public event EventHandler Paused;
        public event EventHandler Stoped;
        public event EventHandler<TrackListLadedEventArgs> TrackListLoaded;

        #endregion

        #region Property
        public bool IsSuttle { get; set; }

        public TrackList TrackList
        {
            get
            {
                return _trackList.IsEmpty ? null : _trackList;
            }
            set
            {
                if (value != null)
                    _trackList = value;
                OnTrackListLoaded(new TrackListLadedEventArgs(_trackList));
            }
        }

        /// <summary>
        /// Возвращает и задает алгоритм повтора треков
        /// </summary>
        public RepeatMode IsRepeat { get; set; }
        /// <summary>
        /// Возвращает и задает состояния проигрывания в настоящий момент
        /// </summary>
        public bool IsPlaying { get; set; }

        #endregion

        #region Outher Func

        /// <summary>
        /// Вычисляет псевдослучайное число
        /// </summary>
        private void Shuttle()
        {
            if (TrackList.IsEmpty) return;
            var rnd = new Random(TrackList.Count);
            TrackList.CurPos = (uint) rnd.Next(0, TrackList.Count);
        }

        #endregion

        #region Event Invocators

        protected virtual void OnTrackListLoaded(TrackListLadedEventArgs e)
        {
            TrackListLoaded?.Invoke(this, e);
        }

        #endregion

    }
}