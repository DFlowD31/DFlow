using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPortal
{
    public class movie
    {
        //Columns
        public Nullable<Int64> id { get; set; } = null; //AutoIncrement
        public string IMDb { get; set; }
        public string name { get; set; }
        public Nullable<Int64> source { get; set; } = null;
        public Nullable<Int64> quality { get; set; } = null;
        public string torrent { get; set; }
        public string subtitle_link { get; set; }
        public Nullable<Int64> encoder { get; set; } = null;
        public Nullable<Decimal> size { get; set; } = null;
        public string type { get; set; }
        public Nullable<Int64> audio_codec { get; set; } = null;
        public Nullable<Int64> audio_channel { get; set; } = null;
        public Nullable<Int64> video_codec { get; set; } = null;
        public string video_bitrate { get; set; }

        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>() { "id" };
        public string database_name { get; } = "movies";
        public List<string> Clauses { get; } = new List<string> { "id" };
    }
}