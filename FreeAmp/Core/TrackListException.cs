using System;
using System.Collections;

namespace FreeAmp.Core
{
    public class TrackListException : Exception
    {
        public TrackListException(string message)
        {
            Message = message;
        }

        public TrackListException(string message, IDictionary data)
        {
            Message = message;
            Data = data;
        }

        public override string Message { get; }

        public override IDictionary Data { get; }
        public override string Source { get; set; }
    }
}