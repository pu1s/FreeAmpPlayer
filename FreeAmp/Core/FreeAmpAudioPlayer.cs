using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace FreeAmp.Core
{
    public class FreeAmpAudioPlayer
    {
        public FreeAmpAudioPlayer()
        {
            TrackList = TrackList.Empty;
            ContentDevice = _defaultDevice;
        }
        public FreeAmpAudioPlayer(TrackList trackList): this()
        {
            TrackList = trackList;
        }
        public FreeAmpAudioPlayer(IWavePlayer contentDevice, TrackList trackList) : this()
        {
            TrackList = trackList;
            ContentDevice = contentDevice;
        }
        private readonly IWavePlayer _defaultDevice = new WaveOut(IntPtr.Zero);

        public FreeAmpAudioPlayer SetDevice(IWavePlayer contentDevise)
        {
            ContentDevice = contentDevise;
            return this;
        }

        public FreeAmpAudioPlayer SetTrackList(TrackList trackList)
        {
            TrackList = trackList;
            return this;
        }

        public AudioFileReader AudioFilereader { get; set; }

        public TrackList TrackList { get; set; }

        public IWavePlayer ContentDevice { get; set; }
    }
}
