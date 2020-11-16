using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFlow
{
    public class tv_series_season
    {
        //Columns
        public Nullable<Int64> tv_series_id { get; set; } = null;
        public Nullable<Decimal> season_number { get; set; } = null;
        public Nullable<Decimal> episode_count { get; set; } = null;
        public string season_name { get; set; }

        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>();
        public string database_name { get; } = "tv_series_seasons";
        public List<string> Clauses { get; } = new List<string> { "tv_series_id", "season_number" };
    }
}