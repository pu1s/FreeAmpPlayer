using System;

namespace FreeAmp.Core
{
    /// <summary>
    ///     ��������� ������������ �����������
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        ///     ���� ����
        /// </summary>
        TrackList TrackList { get; set; }

        /// <summary>
        ///     ���������� � ������ ����� ���������������
        /// </summary>
        bool IsSuttle { get; set; }

        /// <summary>
        ///     ���������� � ������ ����� ���������� ���������������
        /// </summary>
        RepeatMode IsRepeat { get; set; }

        /// <summary>
        ///     ���������� � ������ ���� �� ��������������� � ��������� ������
        /// </summary>
        bool IsPlaying { get; set; }

        /// <summary>
        ///     �������� ������������
        /// </summary>
        void Play();

        /// <summary>
        ///     ���������������� ������������
        /// </summary>
        void Pause();

        /// <summary>
        ///     ������������ ������������
        /// </summary>
        void Resume();

        /// <summary>
        ///     ������������� ������������
        /// </summary>
        void Stop();

        /// <summary>
        ///     ������������ ������� � ���������� ����� � ���� �����
        /// </summary>
        void Next();

        /// <summary>
        ///     ������������ ������� � ����������� ����� � ���� �����
        /// </summary>
        void Preview();

        /// <summary>
        ///     �������, ����������� ��� ������ ������������
        /// </summary>
        event EventHandler Playing;

        /// <summary>
        ///     �������, ����������� ��� ����������������� ������������
        /// </summary>
        event EventHandler Paused;

        /// <summary>
        ///     �������, ����������� ��� ��������� ���������������
        /// </summary>
        event EventHandler Stoped;
    }
}