using System;
using System.Linq;
using NAudio.Wave;

namespace freeampcorelib.FreeAmpAudioRenderDevice
{
    public class FreeAmpOutDevice<T> : IDisposable where T : IWavePlayer, new()
    {
        private T outDevice;

        public PlaybackState PlaybackState => outDevice?.PlaybackState ?? 0;

        public bool IsAviable => GetDeviceAviable();

        private bool GetDeviceAviable()
        {
            if (outDevice is WaveOut)
            {
                return WaveOut.DeviceCount > 0;
            }
            if (outDevice is DirectSoundOut)
            {
                return DirectSoundOut.Devices.Any();
            }
            if (outDevice is AsioOut)
            {
                return AsioOut.isSupported();
            }
            if (outDevice is WasapiOut)
            {
                return Environment.OSVersion.Version.Major > 6;
            }
            return false;
        }

        public FreeAmpOutDevice<T> Create()
        {
            outDevice = new T();
            outDevice.PlaybackStopped += OutDevice_PlaybackStopped;
            return this;
        }

        private void OutDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Init(IWaveProvider waveProvider)
        {
            outDevice.Init(waveProvider);
        }

        public void Play()
        {
            outDevice.Play();
        }

        #region IDisposable Support

        private bool disposedValue; // Для определения избыточных вызовов

        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                    outDevice.PlaybackStopped -= OutDevice_PlaybackStopped;
                    outDevice.Dispose();
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.
                outDevice = default(T);
                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        ~FreeAmpOutDevice()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(false);
        }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            GC.SuppressFinalize(this);
        }

        #endregion
    }



}
