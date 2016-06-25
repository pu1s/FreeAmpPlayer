using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using NAudio.Lame;
using NAudio.Wave;

namespace FreeAmp.Core
{
    [Serializable]
    public class TrackInfo
    {
        public string Title { get; set; }
        public uint TrackNumber { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Labels { get; set; }
        public double Duration { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime LastPlaying { get; set; }
        public Id3V1TagInfo Id3V1TagInfo { get; set; }
    }

    public class Id3V1TagInfo
    {
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Comment { get; set; }
        public uint TrackNumber { get; set; }
    }

    public static class TrackInfoFactory
    {
        private static string RemoveWhiteSpace(string s)
        {
            var newstring = s.Where(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSeparator(c)).Aggregate("", (current, c) => current + c);
            return newstring.Trim();
        }

        #region Genres

        private static readonly string[] audioGenres =
        {
            "Blues",
            "Classic Rock",
            "Country",
            "Dance",
            "Disco",
            "Funk",
            "Grunge",
            "Hip-Hop",
            "Jazz",
            "Metal",
            "New Age",
            "Oldies",
            "Other",
            "Pop",
            "R&B",
            "Rap",
            "Reggae",
            "Rock",
            "Techno",
            "Industrial",
            "Alternative",
            "Ska",
            "Death Metal",
            "Pranks",
            "Soundtrack",
            "Euro-Techno",
            "Ambient",
            "Trip-Hop",
            "Vocal",
            "Jazz+Funk",
            "Fusion",
            "Trance",
            "Classical",
            "Instrumental",
            "Acid",
            "House",
            "Game",
            "Sound Clip",
            "Gospel",
            "Noise",
            "Alternative Rock",
            "Bass",
            "Soul",
            "Punk",
            "Space",
            "Meditative",
            "Instrumental Pop",
            "Instrumental Rock",
            "Ethnic",
            "Gothic",
            "Darkwave",
            "Techno-Industrial",
            "Electronic",
            "Pop-Folk",
            "Eurodance",
            "Dream",
            "Southern Rock",
            "Comedy",
            "Cult",
            "Gangsta",
            "Top 40",
            "Christian Rap",
            "Pop/Funk",
            "Jungle",
            "Native American",
            "Cabaret",
            "New Wave",
            "Psychedelic",
            "Rave",
            "Showtunes",
            "Trailer",
            "Lo-Fi",
            "Tribal",
            "Acid Punk",
            "Acid Jazz",
            "Polka",
            "Retro",
            "Musical",
            "Rock & Roll",
            "Hard Rock",
            "Folk",
            "Folk/Rock",
            "National Folk",
            "Swing",
            "Fusion",
            "Bebob",
            "Latin",
            "Revival",
            "Celtic",
            "Bluegrass",
            "Avantgarde",
            "Gothic Rock",
            "Progressive Rock",
            "Psychedelic Rock",
            "Symphonic Rock",
            "Slow Rock",
            "Big Band",
            "Chorus",
            "Easy Listening",
            "Acoustic",
            "Humour",
            "Speech",
            "Chanson",
            "Opera",
            "Chamber Music",
            "Sonata",
            "Symphony",
            "Booty Bass",
            "Primus",
            "Porn Groove",
            "Satire",
            "Slow Jam",
            "Club",
            "Tango",
            "Samba",
            "Folklore",
            "Ballad",
            "Power Ballad",
            "Rhythmic Soul",
            "Freestyle",
            "Duet",
            "Punk Rock",
            "Drum Solo",
            "A Cappella",
            "Euro-House",
            "Dance Hall",
            "Goa",
            "Drum & Bass",
            "Club-House",
            "Hardcore",
            "Terror",
            "Indie",
            "BritPop",
            "Negerpunk",
            "Polsk Punk",
            "Beat",
            "Christian Gangsta Rap",
            "Heavy Metal",
            "Black Metal",
            "Crossover",
            "Contemporary Christian",
            "Christian Rock",
            "Merengue",
            "Salsa",
            "Thrash Metal",
            "Anime",
            "Jpop",
            "Synthpop"
        };

        #endregion

        private static bool ReadTrackId3V1Tag(string file, ref Id3V1TagInfo ti)
        {
            var hasTag = false;
            var buffer = new byte[128];
            if (!File.Exists(file))
            {
                hasTag = false;
            }
            try
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {

                    if (fs.Length != 0 && fs.Length > 128)
                    {
                        fs.Seek(-128, SeekOrigin.End);
                        fs.Read(buffer, 0, 128);
                    }
                }
                var tagstring = Encoding.UTF8.GetString(buffer);
                if (tagstring.Substring(0, 3) == "TAG")
                {
                    hasTag = true ;
                    ti.Title = RemoveWhiteSpace(tagstring.Substring(3, 30));
                    ti.Artist = RemoveWhiteSpace(tagstring.Substring(33, 30));
                    ti.Album = RemoveWhiteSpace(tagstring.Substring(63, 30));
                    ti.Year = RemoveWhiteSpace(tagstring.Substring(93, 4));
                    ti.Comment = RemoveWhiteSpace(tagstring.Substring(97, 28));
                    if (buffer[125] == 0)
                    {
                        ti.TrackNumber = (uint) buffer[126];
                    }
                    else
                    {
                        ti.TrackNumber = 0;
                    }
                    ti.Genre = audioGenres[buffer[127]];
                }
            }
            catch
            {
                //TODO: Обработать
            }
            return hasTag;
        }

        public static TrackInfo GetTrackInfo(string file)
        {
            var ti = new TrackInfo();
            if (File.Exists(file))
            {
                FileInfo fi = new FileInfo(file);
                ti.FileName = fi.Name;
                ti.FilePath = fi.FullName;
                ti.Duration = fi.Length;
            }
          
            var id3v1tag = new Id3V1TagInfo();
            if (ReadTrackId3V1Tag(file, ref id3v1tag))
            {
                ti.Id3V1TagInfo = id3v1tag;
            }
           
           return ti;
        }

    }
}
