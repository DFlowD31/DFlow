using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFlow
{
    public class anime_movie
    {
        //Columns
        public Nullable<Int64> anime_id { get; set; } = null;
        public Nullable<Decimal> movie_number { get; set; } = null;
        public string english_name { get; set; }
        public string japanese_name { get; set; }
        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>();
        public string database_name { get; } = "anime_movies";
        public List<string> Clauses { get; } = new List<string> { "anime_id", "movie_number" };
    }
}