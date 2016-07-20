using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace freeampcorelib.FreeAmpAudioRenderDevice
{
    public interface IFreeAmpAudioRenderDevice
    {
        void Init(IWaveProvider waveProvider);
        void Play();
        void Pause();
        void Resume();
        void Stop();
        long GetPosition();
        void Dispose();
        PlaybackState PlaybackState { get; }
        float Volume { get; set; }
        event EventHandler<StoppedEventArgs> PlaybackStopped;
    }

    public class FAWO : WaveOut, IFreeAmpAudioRenderDevice
    {
        public FAWO()
        {
            
        }

        public FAWO(WaveCallbackInfo callbackInfo) : base(callbackInfo)
        {
           
        }

        public FAWO(IntPtr windowHandle) : base(windowHandle)
        {
        }

        public new void Play()
        {
            base.Play();
        }
    }
    public class FAOutDev<T> where T : IFreeAmpAudioRenderDevice, new ()
    {
        private T device = new T();

        public float Vol => device?.Volume ?? 0;

        public float Pos => device?.GetPosition() ?? 0;

        public PlaybackState PlaybackState => device?.PlaybackState ?? 0;

        public void Play()
        {
            device.Play();
        }
       
    }

    class MyClass
    {
         FAOutDev<FAWO> a = new FAOutDev<FAWO>();

        private MyClass()
        {
            a.Play();
        }
       
    }
    
}
