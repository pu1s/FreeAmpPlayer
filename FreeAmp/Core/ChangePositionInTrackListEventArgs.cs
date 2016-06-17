using System;

namespace FreeAmp.Core
{
    public class ChangePositionInTrackListEventArgs : EventArgs
    {
        private uint _curPos;

        public uint CurPos => _curPos;

        public ChangePositionInTrackListEventArgs(uint pos)
        {
            _curPos = pos;
        }
    }
}