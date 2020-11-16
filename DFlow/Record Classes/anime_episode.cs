using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFlow
{
    public class anime_episode
    {
        //Columns
        public Nullable<Int64> anime_id { get; set; } = null;
        public Nullable<Int64> season_number { get; set; } = null;
        public Nullable<Decimal> episode_number { get; set; } = null;
        public string english_name { get; set; }
        public string japanese_name { get; set; }
        public Nullable<Int64> encoder { get; set; } = null;
        public Nullable<Int64> quality { get; set; } = null;
        public Nullable<Int64> source { get; set; } = null;
        public string torrent_file { get; set; }

        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>();
        public string database_name { get; } = "anime_episodes";
        public List<string> Clauses { get; } = new List<string> { "anime_id", "season_number", "episode_number" };
    }
}