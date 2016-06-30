using System;
using System.IO;
using System.Linq;
using System.Text;

namespace freeampcorelib
{
    #region Represent
    /// <summary>
    /// Данные, содержащиеся в теге ID3v1 аудио файла
    /// </summary>
    [Obsolete]
    public class Id3v1TagInfo
    {
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Comment { get; set; }
        public uint TrackNumber { get; set; }
    }
    [Obsolete]
    public static class TagReader
    {
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

        /// <summary>
        ///     Убирает нулевые символы
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
      
        private static string RemoveWhiteSpace(string s)
        {
            var newstring =
                s.Where(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSeparator(c))
                    .Aggregate("", (current, c) => current + c);
            return newstring.Trim();
        }

        /// <summary>
        ///     Читает тег ID3v1
        /// </summary>
        /// <param name="tag">
        ///     байты тега
        /// </param>
        /// <returns>
        ///     Форматированная информация
        /// </returns>
        /// 
        [Obsolete]
        public static Id3v1TagInfo Id3v1TagReader(byte[] tag)
        {
            var tagInfo = new Id3v1TagInfo();
            var buffer = tag;
            var tagstring = Encoding.UTF8.GetString(buffer);
            if (tagstring.Substring(0, 3) == "TAG")
            {
                tagInfo.Title = RemoveWhiteSpace(tagstring.Substring(3, 30));
                tagInfo.Artist = RemoveWhiteSpace(tagstring.Substring(33, 30));
                tagInfo.Album = RemoveWhiteSpace(tagstring.Substring(63, 30));
                tagInfo.Year = RemoveWhiteSpace(tagstring.Substring(93, 4));
                tagInfo.Comment = RemoveWhiteSpace(tagstring.Substring(97, 28));
                tagInfo.TrackNumber = buffer[125] == 0 ? (uint)buffer[126] : 0;

                tagInfo.Genre = buffer[127] > 147 ? string.Empty : audioGenres[buffer[127]];

            }
            return tagInfo;
        }

        //public static Id3TagInfo Id3v2TagReader(byte[] tag)
        //{
        //    //TODO: написать определение
        //    return new Id3TagInfo();
        //}
        [Obsolete]
        public static bool ReadTrackId3V1Tag(string file, ref Id3v1TagInfo ti)
        {
            var hasTag = false;
            var buffer = new byte[128];
            if (!File.Exists(file))
            {
                return false;
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
                    hasTag = true;
                    ti.Title = RemoveWhiteSpace(tagstring.Substring(3, 30));
                    ti.Artist = RemoveWhiteSpace(tagstring.Substring(33, 30));
                    ti.Album = RemoveWhiteSpace(tagstring.Substring(63, 30));
                    ti.Year = RemoveWhiteSpace(tagstring.Substring(93, 4));
                    ti.Comment = RemoveWhiteSpace(tagstring.Substring(97, 28));
                    if (buffer[125] == 0)
                    {
                        ti.TrackNumber = buffer[126];
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
    }
    [Obsolete]
    public class Id3v2TagInfo
    {

    }

    #endregion

}
