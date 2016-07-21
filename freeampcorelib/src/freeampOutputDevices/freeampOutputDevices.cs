using System;
using NAudio.Wave;

namespace freeampcorelib.freeampOutputDevices
{
     public class FreeAmpOutputDevices
    {

    }

    public class FreeAmpOutputDevice
    {
        
    }

    public interface IFreeAmpOutputDevice : IDisposable
    {
        float Volume { get; set; }
        FreeAmpOutputDeviceState State { get; }

    }

    public enum FreeAmpOutputDeviceState
    {
        Playing,
        Paused,
        Stopped,
        Unknown
    };

}
