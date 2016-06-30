using System.Collections.Generic;


namespace freeampcorelib
{
    /// <summary>
    /// ���������� ���� �����
    /// </summary>
    public class TrackList
    {
        private uint _curPosition;
        private bool _isEmpty;
        private readonly List<Track> _trackList;
        /// <summary>
        /// ���������� ������ ������ � ���� �����
        /// </summary>
        public List<Track> Items => _trackList;

        /// <summary>
        ///     ����������� �� ���������
        /// </summary>
        public TrackList()
        {
            _trackList = new List<Track>();
            _isEmpty = true;
            _curPosition = 0;
        }
        
        /// <summary>
        ///     ���������� � ������ "������� �������" ����� <see cref="Track" /> � ���� ����� <see cref="TrackList" />
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
        ///     ���������� ������� ������ ����� �� �������
        /// </summary>
        /// <param name="index">
        ///     ������
        /// </param>
        /// <returns>
        ///     ������ � ����� <see cref="Track" />
        /// </returns>
        public Track this[int index] => _isEmpty ? null : _trackList[index];

        /// <summary>
        ///     ���������� ��������� ���� �����
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
        /// ���������� ������ ���� ����
        /// </summary>
        public static TrackList Empty { get; }
        /// <summary>
        /// ���������� ������� ���� <see cref="Track"/> � ���� ����� <see cref="TrackList"/>
        /// </summary>
        /// <returns></returns>
        public Track GetCurrentTrack() => IsEmpty ? null : _trackList[(int) _curPosition];
    }
}