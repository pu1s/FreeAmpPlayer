using System;

namespace FreeAmp.Core
{
    /// <summary>
    ///     »нтерфейс проигрывани€ мультимедиа
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        ///     “рек лист
        /// </summary>
        TrackList TrackList { get; set; }

        /// <summary>
        ///     ¬озвращает и задает режим воспроизведени€
        /// </summary>
        bool IsSuttle { get; set; }

        /// <summary>
        ///     ¬озвращает и задает режим повторного воспроизведени€
        /// </summary>
        RepeatMode IsRepeat { get; set; }

        /// <summary>
        ///     ¬озвращант и задает идет ли воспроизведение в насто€щий момент
        /// </summary>
        bool IsPlaying { get; set; }

        /// <summary>
        ///     Ќачинает проигрывание
        /// </summary>
        void Play();

        /// <summary>
        ///     ѕриостанавливает проигрывание
        /// </summary>
        void Pause();

        /// <summary>
        ///     ¬озобновл€ет проигрывание
        /// </summary>
        void Resume();

        /// <summary>
        ///     ќстанавливает проигрывание
        /// </summary>
        void Stop();

        /// <summary>
        ///     ќсуществл€ет переход к следующему треку в плей листе
        /// </summary>
        void Next();

        /// <summary>
        ///     ќсуществл€ет переход к предидущему треку в плей листе
        /// </summary>
        void Preview();

        /// <summary>
        ///     —обытие, возникающее при начале проигрывани€
        /// </summary>
        event EventHandler Playing;

        /// <summary>
        ///     —обытие, возникающее при приостанавливании проигрывани€
        /// </summary>
        event EventHandler Paused;

        /// <summary>
        ///     —обытие, возникающее при остановке воспроизведени€
        /// </summary>
        event EventHandler Stoped;
    }
}