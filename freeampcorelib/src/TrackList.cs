using System.Collections.Generic;


namespace freeampcorelib
{
    /// <summary>
    /// Реализация трек листа
    /// </summary>
    public class TrackList
    {
        private uint _curPosition;
        private bool _isEmpty;
        private readonly List<Track> _trackList;
        /// <summary>
        /// Возвращает список треков в трек листе
        /// </summary>
        public List<Track> Items => _trackList;

        /// <summary>
        ///     Конструктор по умолчанию
        /// </summary>
        public TrackList()
        {
            _trackList = new List<Track>();
            _isEmpty = true;
            _curPosition = 0;
        }
        
        /// <summary>
        ///     Возвращает и задает "текущую позицию" трека <see cref="Track" /> в трек листе <see cref="TrackList" />
        /// </summary>
        public uint CurrentPosition
        {
            get { return _curPosition; }
            set
            {
                if (value >= _trackList.Count && value <= _trackList.Count)
                {
                    _curPosition = value;
                }
            }
        }

        /// <summary>
        ///     Возвращает элемент списка листа по индексу
        /// </summary>
        /// <param name="index">
        ///     Индекс
        /// </param>
        /// <returns>
        ///     Данные о треке <see cref="Track" />
        /// </returns>
        public Track this[int index] => _isEmpty ? null : _trackList[index];

        /// <summary>
        ///     Возвращает состояние трек листа
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                _isEmpty = _trackList.Count == 0;
                return _isEmpty;
            }
        }
        /// <summary>
        /// Определяет пустой трек лист
        /// </summary>
        public static TrackList Empty { get; }
        /// <summary>
        /// Возвращает текущий трек <see cref="Track"/> в трек листе <see cref="TrackList"/>
        /// </summary>
        /// <returns></returns>
        public Track GetCurrentTrack() => IsEmpty ? null : _trackList[(int) _curPosition];
    }
}