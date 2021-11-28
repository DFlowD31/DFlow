using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LazyPortal.Classes;
using Newtonsoft.Json;

namespace LazyPortal.services
{
    public static class msgType
    {
        public static uint success = 0xFF32CD32; /// LimeGreen
        public static uint message = 0xFF000000; /// Black
        public static uint warning = 0xFFFF8C00; /// DarkOrange
        public static uint error = 0xFFDC143C;   /// Crimson
        public static uint baseColor = 0xFF00AEDB; /// Base App color
    }

    public static class mediaType
    {
        //private static readonly Dictionary<int, Tuple<string, dynamic>> typeCollection = new Dictionary<int, Tuple<string, dynamic>>() { { 0, new Tuple<string, dynamic>("first_air_date_year", TMDB_collection) }, { 1, new Tuple<string, string>("year", "TMDB_movie") }, { 4, new Tuple<string, string>("year", "TMDB_tv") } };
        private static readonly Dictionary<int, string> typeCollection = new Dictionary<int, string>() { { 0, "first_air_date_year" }, { 1, "year" }, { 4, "year" } };
        public static string getDateQuery(int type)
        {
            return typeCollection[type];
        }

        public static dynamic getResponce(int type, string content)
        {
            if (type == 0)
                return JsonConvert.DeserializeObject<TMDB_collection>(content);
            else if (type == 1)
                return JsonConvert.DeserializeObject<TMDB_movie>(content);
            else
                return JsonConvert.DeserializeObject<TMDB_tv>(content);
        }
    }

    public enum mediaTypes : int
    {
        tv = 0,
        movie = 1,
        anime = 2,
        manga = 3,
        collection = 4
    }

    public static class extension
    {
        public static string[] subtitleEXT = new string[] { ".aaf", ".asc", ".bmp", ".cap", ".cin", ".idx", ".itt", ".mcc", ".nav", ".onl", ".sami", ".sbv", ".scc", ".scr", ".smi", ".son", ".srt", ".sst", ".stl", ".sub", ".tif", ".ult", ".vtt", ".xml" };
        public static string[] videoEXT = new string[] { ".3g2", ".3gp", ".amv", ".asf", ".avi", ".drc", ".f4a", ".f4b", ".f4p ", ".f4v", ".flv", ".gif", ".gifv", ".m2ts", ".m2v", ".m4p", ".m4v", ".mkv", ".mng", ".mov", ".mp2", ".mp4", ".mpe", ".mpeg", ".mpg", ".mpv", ".mts", ".mxf", ".nsv", ".ogg", ".ogv", ".qt", ".rm", ".rmvb", ".roq", ".svi", ".ts", ".viv", ".vob", ".webm", ".wmv", ".yuv" };
        public static string[] audioEXT = new string[] { ".3gp", ".8svx", ".aa", ".aac", ".aax", ".act", ".aiff", ".alac", ".amr", ".ape", ".au", ".awb", ".cda", ".dss", ".dvf", ".flac", ".gsm", ".iklax", ".ivs", ".m4a", ".m4b", ".m4p", ".mmf", ".mogg", ".mp3", ".mpc", ".msv", ".nmf", ".oga", ".ogg", ".opus", ".ra", ".raw", ".rf64", ".rm", ".sln", ".tta", ".voc", ".vox", ".wav", ".webm", ".wma", ".wv" };
        public static string[] imageEXT = new string[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
    }

    public static class choice
    {
        public static bool series = false;
        public static bool container = false;
        public static bool third = false;
        public static language chosenLanguage;
        public static string inputText = string.Empty;
    }

    public enum language : int
    {
        english = 0,
        japanese = 1
    }
}
