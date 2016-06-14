using System;

namespace FreeAmp.Core
{
    public class ChangePosEventArgs : EventArgs
    {
        private uint _curPos;

        public uint CurPos => _curPos;

        public ChangePosEventArgs(uint pos)
        {
            _curPos = pos;
        }
    }
}