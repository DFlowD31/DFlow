using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPortal
{
    public class tv_series_episode
    {
        //Columns
        public Nullable<Int64> tv_series_id { get; set; } = null;
        public Nullable<Decimal> season_number { get; set; } = null;
        public Nullable<Decimal> episode_number { get; set; } = null;
        public string name { get; set; }
        public Nullable<Int64> source { get; set; } = null;
        public Nullable<Int64> quality { get; set; } = null;
        public Nullable<Int64> encoder { get; set; } = null;
        public Nullable<Int64> video_codec { get; set; } = null;
        public string video_bitrate { get; set; }
        public Nullable<Int64> audio_codec { get; set; } = null;
        public Nullable<Int64> audio_channel { get; set; } = null;

        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>();
        public string database_name { get; } = "tv_series_episodes";
        public List<string> Clauses { get; } = new List<string> { "tv_series_id", "season_number", "episode_number" };
    }
}