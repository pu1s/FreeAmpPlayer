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

    [Serializable]
    public class Playlist
    {
        public Playlist()
        {
            Tracks = new List<Track>();
        }

        public Playlist(string name) : this()
        {
            Name = name;
        }

        public Playlist(string name, List<Track> tracks) : this(name)
        {
            Tracks = tracks;
        }

        public string Name { get; set; }

        public List<Track> Tracks { get; private set; }

        public void Add(Track track)
        {
            Tracks.Add(track);
        }

        public void Add(IEnumerable<Track> tracks)
        {
            Tracks.AddRange(tracks.ToList());
        }

        public void Remove(Track track)
        {
            if (Tracks.Count == 0) throw new ArgumentOutOfRangeException(@"Tracks");
            if (Tracks.Contains(track)) Tracks.Remove(track);
        }

        public void Remove(int index)
        {
            if (Tracks.Count == 0 || Tracks.Count < index) throw new ArgumentOutOfRangeException(@"index");
            Tracks.RemoveAt(index);
        }

        public void Remove(IEnumerable<Track> tracks)
        {
            if (Tracks.Count == 0) throw new ArgumentOutOfRangeException(@"Tracks");
            var enumerable = tracks as IList<Track> ?? tracks.ToList();
            if(tracks == null || !enumerable.Any()) throw new ArgumentOutOfRangeException(@"tracks");
            foreach (var track in enumerable.Where(track => Tracks.Contains(track)))
            {
                Tracks.Remove(track);
            }
        }
    }

    public class Track
    {
        public string TrackName { get; set; }
    }
}