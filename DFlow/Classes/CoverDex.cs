using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPortal
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CoverDex
    {
        public string result { get; set; }
        public string response { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string id { get; set; }
            public string type { get; set; }
            public Attributes attributes { get; set; }
            public List<Relationship> relationships { get; set; }
        }

        public class Attributes
        {
            public string description { get; set; }
            public string volume { get; set; }
            public string fileName { get; set; }
            public string locale { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime updatedAt { get; set; }
            public int version { get; set; }
        }

        public class Relationship
        {
            public string id { get; set; }
            public string type { get; set; }
        }
    }
}