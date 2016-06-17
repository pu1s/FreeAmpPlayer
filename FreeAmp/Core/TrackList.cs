using System;
using System.Collections.Generic;

namespace FreeAmp.Core
{
    /// <summary>
    ///     Список треков
    /// </summary>
    public class TrackList
    {
        /// <summary>
        ///     статическое поле, показывающее заполнение треклиста
        /// </summary>
        private static bool _isEmpty;

        /// <summary>
        ///     Состояние листа (пустой лист)
        /// </summary>
        public static readonly TrackList Empty = EmptyTrackList();

        /// <summary>
        ///     указатель на текущий трек
        /// </summary>
        private uint _curPos;

        /// <summary>
        ///     Статический конструктор
        /// </summary>
        static TrackList()
        {
            _isEmpty = true;
        }

        /// <summary>
        ///     Приватный конструктор
        /// </summary>
        private TrackList()
        {
            Tracks = new List<Track>();
        }

        /// <summary>
        ///     Конструктор с указанием имени трек листа
        /// </summary>
        /// <param name="name">
        ///     Имя
        /// </param>
        public TrackList(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        ///     Возвращает состояние заполненности трек листа
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                _isEmpty = Tracks.Count == 0;
                return _isEmpty;
            }
            private set { _isEmpty = value; }
        }

        /// <summary>
        ///     Возвращает и присваивает имя трек листа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Список треков
        /// </summary>
        public List<Track> Tracks { get; }

        /// <summary>
        ///     Количество элементов списка треков
        /// </summary>
        public int Count => Tracks.Count;

        /// <summary>
        ///     Возвращает указатель на текущий трек
        /// </summary>
        public uint CurPos
        {
            get { return Tracks.Count == 0 ? 0 : _curPos; }
            set
            {
                if (value >= Tracks.Count) throw new ArgumentOutOfRangeException();
                if (_curPos == value) return;
                _curPos = value;
                OnChangeCurPosition(_curPos);
            }
        }

        /// <summary>
        ///     Перечислитель коллекции
        /// </summary>
        /// <param name="index">
        ///     Индекс
        /// </param>
        /// <returns>
        ///     Трек
        /// </returns>
        public Track this[int index]
        {
            get
            {
                if (_isEmpty)
                    return null;
                if (index < 0 && index >= Tracks.Count)
                    return null;
                return Tracks[index];
            }
        }

        /// <summary>
        ///     Статическая функция, реализующая "обнуление списка"
        /// </summary>
        /// <returns>
        ///     <see cref="TrackList" />
        /// </returns>
        private static TrackList EmptyTrackList()
        {
            var tr = new TrackList {IsEmpty = _isEmpty = true};
            return tr;
        }

        /// <summary>
        ///     Добавляет трек <see cref="Track" />в конец списка
        /// </summary>
        /// <param name="track">
        ///     Трек
        /// </param>
        public void AppendTrack(Track track)
        {
            Tracks.Add(track);
        }

        /// <summary>
        ///     Добавляет массив треков <see cref="Track" />в конец списка
        /// </summary>
        /// <param name="tracks">
        ///     Массив треков <see cref="Track" />
        /// </param>
        public void AppendTracks(Track[] tracks)
        {
            Tracks.AddRange(tracks);
        }

        /// <summary>
        ///     Добавляет коллекцию треков <see cref="Track" /> в конец списка
        /// </summary>
        /// <param name="tracks">
        ///     Коллекция треков <see cref="Track" />
        /// </param>
        public void AppendTracks(IEnumerable<Track> tracks)
        {
            Tracks.AddRange(tracks);
        }

        /// <summary>
        ///     Перемещает указатель трека <see cref="Track" /> на одну позицию вперед
        /// </summary>
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public void NextTrack()
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            if (CurPos == Tracks.Count - 1)
            {
                throw new TrackListException("Список воспроизведения достиг конечной точки!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            CurPos++;
        }

        /// <summary>
        ///     Перемещает указатель трека <see cref="Track" /> на одну позицию назад
        /// </summary>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public void PreviewTrack()
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            if (CurPos == 0)
            {
                throw new TrackListException("Список воспроизведения достиг конечной точки!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            CurPos--;
        }

        /// <summary>
        ///     Добаляет трек в указанное место в списке <see cref="TrackList" />
        /// </summary>
        /// <param name="track">
        ///     Трек <see cref="Track" />
        /// </param>
        /// <param name="pos">
        ///     Позиция, в которую осуществляется добавление
        /// </param>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public void InsertTrack(Track track, int pos)
        {
            if (pos < 0 && pos > Tracks.Count)
            {
                throw new TrackListException("Невозможно вставит указанный трек в указанную позицию");
            }
            Tracks.Insert(pos, track);
        }

        /// <summary>
        ///     Добаляет треки в указанное место в списке <see cref="TrackList" />
        /// </summary>
        /// <param name="tracks">
        ///     Массив треков <see cref="Track" />
        /// </param>
        /// <param name="pos">
        ///     Позиция, в которую осуществляется добавление
        /// </param>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public void InsertTracks(Track[] tracks, int pos)
        {
            if (pos < 0 && pos > Tracks.Count)
            {
                throw new TrackListException("Невозможно вставит указанный трек в указанную позицию");
            }
            Tracks.InsertRange(pos, tracks);
        }

        /// <summary>
        ///     Добаляет треки в указанное место в списке <see cref="TrackList" />
        /// </summary>
        /// <param name="tracks">
        ///     Коллекция треков <see cref="Track" />
        /// </param>
        /// <param name="pos">
        ///     Позиция, в которую осуществляется добавление
        /// </param>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public void InsertTracks(IEnumerable<Track> tracks, int pos)
        {
            if (pos < 0 && pos > Tracks.Count)
            {
                throw new TrackListException("Невозможно вставит указанный трек в указанную позицию");
            }
            Tracks.InsertRange(pos, tracks);
        }

        /// <summary>
        ///     Перемещает трек из одной позиции на другую
        /// </summary>
        /// <param name="beforepos">
        ///     Нальная позиция
        /// </param>
        /// <param name="lastpos">
        ///     Конечная позиция
        /// </param>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public void MovieTrack(int beforepos, int lastpos)
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }

            if (beforepos < 0 && beforepos >= Tracks.Count)
            {
                throw new TrackListException("Неверно указана начальная позиция!");
            }
            if (lastpos < 0 && lastpos >= Tracks.Count)
            {
                throw new TrackListException("Неверно указана конечная позиция!");
            }
            if (beforepos == lastpos) return;
            var track = Tracks[beforepos];
            Tracks.RemoveAt(beforepos);
            InsertTrack(track, lastpos);
        }

        /// <summary>
        ///     Возвращает текущий трек <see cref="Track" /> из списка <see cref="Tracks" />
        /// </summary>
        /// <returns>
        ///     Трек <see cref="Track" />
        /// </returns>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public Track GetCurrentTrack()
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            return Tracks[(int) _curPos];
        }

        /// <summary>
        ///     Возвращает следующий трек <see cref="Track" /> из списка <see cref="Tracks" />
        /// </summary>
        /// <returns>
        ///     Трек <see cref="Track" />
        /// </returns>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public Track GetPreviewTrack()
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            return _curPos == 0 ? null : Tracks[(int) _curPos - 1];
        }

        /// <summary>
        ///     Возвращает следующий трек <see cref="Track" /> из списка <see cref="Tracks" />
        /// </summary>
        /// <returns>
        ///     Трек <see cref="Track" />
        /// </returns>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public Track GetNextTrack()
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            if (_curPos == 0 && _curPos < Tracks.Count)
            {
                return Tracks[(int) _curPos + 1];
            }
            return null;
        }

        /// <summary>
        ///     Очищает текущий трек лист <see cref="TrackList" />
        /// </summary>
        public void ClearTrackList()
        {
            Tracks.Clear();
        }

        /// <summary>
        ///     Удаляет трек <see cref="Track" /> из треклиста <see cref="Tracks" />
        /// </summary>
        /// <param name="track">
        ///     Трек <see cref="Track" />
        /// </param>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public void RemoveTrack(Track track)
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            if (Tracks.Contains(track))
            {
                Tracks.Remove(track);
            }
        }

        /// <summary>
        ///     Удаляет трек <see cref="Track" /> из треклиста <see cref="Tracks" /> по указанному индексу
        /// </summary>
        /// <param name="index">
        ///     Индекс
        /// </param>
        /// ///
        /// <exception cref="TrackListException">
        ///     Исключение, возникающее при ошибке в трек листе <see cref="TrackList" />
        /// </exception>
        public void RemoveTrackAt(int index)
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) {{1, Tracks.Count.ToString()}});
            }
            if (index <= Tracks.Count)
            {
                Tracks.RemoveAt(index);
            }
        }

        public event EventHandler<ChangePosEventArgs> ChangeCurPosition;

        protected virtual void OnChangeCurPosition(uint pos)
        {
            ChangeCurPosition?.Invoke(this, new ChangePosEventArgs(pos));
        }
    }
}