using System;
using System.Collections.Generic;

namespace FreeAmp.Core
{
    public class TrackList
    {
        private static bool _isEmpty;

        public static readonly TrackList Empty = EmptyTrackList();

        private uint curPos;

        static TrackList()
        {
            _isEmpty = true;
        }

        private TrackList()
        {
            Tracks = new List<Track>();
        }

        public bool IsEmpty
        {
            get
            {
                _isEmpty = Tracks.Count == 0;
                return _isEmpty;
            }
            private set { _isEmpty = value; }
        }

        public string Name { get; set; }

        public List<Track> Tracks { get; }

        public int Count => Tracks.Count;

        public uint CurPos
        {
            get { return Tracks.Count == 0 ? 0 : curPos; }
            set
            {
                if (value >= Tracks.Count) throw new ArgumentOutOfRangeException();
                curPos = value;
            }
        }

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

        private static TrackList EmptyTrackList()
        {
            var tr = new TrackList {IsEmpty = _isEmpty = true};
            return tr;
        }

        public void AppendTrack(Track track)
        {
            Tracks.Add(track);
        }

        public void AppendTracks(Track[] tracks)
        {
            Tracks.AddRange(tracks);
        }

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
            curPos++;
        }

        public void PreviewTrack()
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                    new Dictionary<int, string>(1) { { 1, Tracks.Count.ToString() } });
            }
            if (CurPos == 0)
            {
                throw new TrackListException("Список воспроизведения достиг конечной точки!",
                    new Dictionary<int, string>(1) { { 1, Tracks.Count.ToString() } });
            }
            curPos--;
        }

        public void InsertTrack(Track track, int pos)
        {
            if (pos < 0 && pos > Tracks.Count)
            {
                throw new TrackListException("Невозможно вставит указанный трек в указанную позицию");
            }
            Tracks.Insert(pos, track);
        }

        public void InsertTracks(Track[] tracks, int pos)
        {
            if (pos < 0 && pos > Tracks.Count)
            {
                throw new TrackListException("Невозможно вставит указанный трек в указанную позицию");
            }
            Tracks.InsertRange(pos, tracks);
        }

        public void MovieTrack(int beforepos, int lastpos)
        {
            if (IsEmpty)
            {
                throw new TrackListException("Список воспроизведения пуст!",
                   new Dictionary<int, string>(1) { { 1, Tracks.Count.ToString() } });
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
            //TODO реализация метода удаления не выполнена
            InsertTrack(track, lastpos);
        }
      
    }
}