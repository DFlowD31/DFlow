using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFlow
{
    public class source
    {
        //Columns
        public Nullable<Int64> id { get; set; } = null; //AutoIncrement
        public string name { get; set; }

        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>() { "id" };
        public string database_name { get; } = "sources";
        public List<string> Clauses { get; } = new List<string> { "id" };
    }
}