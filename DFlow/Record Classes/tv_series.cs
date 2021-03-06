﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFlow
{
    public class tv_series
    {
        //Columns
        public Nullable<Int64> id { get; set; } = null; //AutoIncrement
        public string IMDb { get; set; }
        public string name { get; set; }
        public string original_release_start { get; set; }
        public string original_release_end { get; set; }

        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>() { "id" };
        public string database_name { get; } = "tv_series";
        public List<string> Clauses { get; } = new List<string> { "id", "IMDb" };
    }
}