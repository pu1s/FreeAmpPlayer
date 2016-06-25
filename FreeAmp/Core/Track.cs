using System;
using System.IO;

namespace FreeAmp.Core
{
    /// <summary>
    ///     Представляет имя и путь к файлу композиции
    /// </summary>
    public class Track 
    {
        private string _path;
        private TrackInfo _trackInfo;

        /// <summary>
        ///     Конструктор по умолчанию
        /// </summary>
        public Track()
        {
            _path = string.Empty;
            _trackInfo = null;
        }

        /// <summary>
        ///     Создает новое описание файла композиции
        /// </summary>
        /// <param path="path">
        ///     Имя и путь к файлу композиции
        /// </param>
        /// <param name="path"></param>
        public Track(string path) : this()
        {
            try
            {
                if (File.Exists(path))
                _path = path;
                //TODO: 
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     Имя и путь к файлу композиции
        /// </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) && File.Exists(value))
                    _path = value;

            }
        }

        public TrackInfo TrackInfo
        {
            get
            {
                return _trackInfo;
            }
            set
            {
                if (value != null)
                {
                    _trackInfo = value;
                }

            }
            
        }
    }
}