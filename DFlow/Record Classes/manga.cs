using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPortal
{
    public class manga
    {
        //Columns
        public Nullable<Int64> id { get; set; } = null; //AutoIncrement
        public string english_name { get; set; }
        public string japanese_name { get; set; }
        public Nullable<Decimal> latest_read_chapter { get; set; }
        public string base_url { get; set; }
        public string mangadex_id { get; set; }
        public string cover_id { get; set; }
        public string cover_file_name { get; set; }

        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>() { "id" };
        public string database_name { get; } = "mangas";
        public List<string> Clauses { get; } = new List<string> { "id" };
    }
}
