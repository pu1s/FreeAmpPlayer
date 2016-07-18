using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NAudio.CoreAudioApi;

namespace freeampcorelib
{
    public static class CoreAudio
    {
        /// <summary>
        ///     Возвращает список активных аудиоустройств в системе
        /// </summary>
        /// <param name="devices">
        /// Устройства вывода звука
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void GetMMDeviceCollection(out List<MMDevice> devices)
        {
            devices = new List<MMDevice>();
            var enumerator = new MMDeviceEnumerator();
            var dev = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            if (dev.Count == 0) throw new ArgumentNullException(nameof(devices));
            devices.AddRange(dev.ToArray());
#if DEBUG
            foreach (var mmDevice in devices)
            {
                Debug.WriteLine(mmDevice.FriendlyName);
            }
#endif
        }

        /// <summary>
        /// Возвращает список активных аудиоустройств в системе
        /// </summary>
        /// <returns>
        /// Коллекция устройств вывода звука
        /// </returns>
        public static MMDeviceCollection GetMmDeviceCollection()
        {
            var enumerator = new MMDeviceEnumerator();
            return enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
        }

       

    }

   
   
}