using System;
using System.IO;
using Id3;

namespace freeampcorelib
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
            if (File.Exists(path))
                _path = path;
            _trackInfo = TrackInfoFactory.GetTrackInfo(_path);
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

        /// <summary>
        ///     Информация о файле
        /// </summary>
        public TrackInfo TrackInfo
        {
            get { return _trackInfo; }
            set
            {
                if (value != null)
                {
                    _trackInfo = value;
                }
            }
        }
    }

    /// <summary>
    ///     Предоставляет методы для загрузки метаданных аудио файлов
    /// </summary>
    public static class TrackInfoFactory
    {
        /// <summary>
        ///     Считывает метаданные из аудио файлов
        /// </summary>
        /// <param name="file">файл</param>
        /// <returns>
        ///     данные о файле <see cref="TrackInfo" />
        /// </returns>
        public static TrackInfo GetTrackInfo(string file)
        {
            var ti = new TrackInfo();

            var fi = new FileInfo(file);

            #region if mp3

            // if file extension
            switch (fi.Extension)
            {
                case ".mp3":
                    //************************************************************************
                    // Obsolete
                    //************************************************************************
                    //using (var fileReader = new Mp3FileReader(file))
                    //{
                    //    ti.FileName = fi.FullName;
                    //    ti.Title = fi.Name;
                    //    ti.Length = fi.Length;
                    //    if (fileReader.Id3v1Tag != null)
                    //    {
                    //        // считываем тег
                    //        ti.Id3V1TagInfo = TagReader.Id3v1TagReader(fileReader.Id3v1Tag);
                    //        //обновляем данные о файле из тега
                    //        ti.Title = ti.Id3V1TagInfo.Title;
                    //        ti.Album = ti.Id3V1TagInfo.Album;
                    //        ti.Artist = ti.Id3V1TagInfo.Artist;
                    //        ti.Comment = ti.Id3V1TagInfo.Comment;
                    //        ti.Genre = ti.Id3V1TagInfo.Genre;
                    //        ti.TrackNumber = ti.Id3V1TagInfo.TrackNumber;
                    //    }
                    //    if (fileReader.Id3v2Tag != null)
                    //    {
                    //        //throw new NotImplementedException();
                    //        //var mp3 = new Mp3Stream(new FileStream(file, FileMode.Open));
                    //         //ti.Id3TagInfo = mp3.GetAllTags();
                    //    }
                    //}

                    //***********************************************************************
                    // Using ID3 lib v0.3.0
                    //***********************************************************************
                    using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        using (var mp3Stream = new Mp3Stream(fileStream))
                        {
                            if (mp3Stream.HasTags)
                            {
                                ti.Id3TagInfo = mp3Stream.GetAllTags();
                                
                            }
                            //TODO: закончить
                        }
                    }
                    break;
                case ".wav":
                    //TODO: добавить обработку wav файлов
                    throw new NotImplementedException();
            }

            #endregion

            return ti;
        }
    }


    /// <summary>
    ///     Метаданные аудио файлов
    /// </summary>
    [Serializable]
    public class TrackInfo
    {
        public string Title { get; set; }
        public uint TrackNumber { get; set; }
        public string FileName { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Labels { get; set; }
        public double Length { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime LastPlaying { get; set; }
        public Id3Tag[] Id3TagInfo { get; set; }
    }
}