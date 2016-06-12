using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;
using NAudio;
using NAudio.CoreAudioApi;

namespace FreeAmp.Core
{
    public class CoreAudio
    {
        /// <summary>
        /// Возвращает список активных аудиоустройств в системе
        /// </summary>
        /// <param name="devices">Устройства вывода звука</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void GetMMDeviceCollection(out List<MMDevice> devices)
        {
            devices = new List<MMDevice>();
            var enumerator = new MMDeviceEnumerator();
            var dev = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            if (dev.Count == 0) throw new ArgumentNullException("devices");
            devices.AddRange(dev.ToArray());
        }
    }

    public class AudioPlayback
    {
    }
    
    
}