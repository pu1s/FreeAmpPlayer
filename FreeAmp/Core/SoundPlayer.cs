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
        ///     �������� ������������
        /// </summary>
        public void Play()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     ���������������� ������������
        /// </summary>
        public void Pause()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     ������������ ������������
        /// </summary>
        public void Resume()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     ������������� ������������
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     ������������ ������� � ���������� ����� � ���� �����
        /// </summary>
        public void Next()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     ������������ ������� � ����������� ����� � ���� �����
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
        /// ���������� � ������ �������� ������� ������
        /// </summary>
        public RepeatMode IsRepeat { get; set; }
        /// <summary>
        /// ���������� � ������ ��������� ������������ � ��������� ������
        /// </summary>
        public bool IsPlaying { get; set; }

        #endregion

        #region Outher Func

        /// <summary>
        /// ��������� ��������������� �����
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