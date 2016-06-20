using System;
using NAudio.Wave;

namespace FreeAmp.Core
{
    public class SoundPlayer : IPlayer
    {
        #region Fields

        private TrackList _trackList;

        #endregion

        #region Main Func

        public void Play()
        { 
            AudioFileReader reader;
           reader =
               new AudioFileReader(TrackList.GetCurrentTrack().Name);
            OutDevice.Init(reader);
            OutDevice.Play();
            IsPlaying = true;
        }

        /// <summary>
        ///     ���������������� ������������
        /// </summary>
        public void Pause()
        {
            OutDevice.Pause();
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
           TrackList.NextTrack();
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

        public TrackList TrackList { get; set; }
        
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
            TrackList.CurPos = (uint)rnd.Next(0, TrackList.Count);
        }

        #endregion

    }

    public enum AudioFileFormat
    {
        wav,
        mp3,
        mp2,
        mp4
    };
}