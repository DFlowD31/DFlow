using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPortal.Classes
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Net;
    using System.Numerics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Chapter
    {
        public int num_entries { get; set; }
    }

    public class Container
    {
        public Properties properties { get; set; }
        public bool recognized { get; set; }
        public bool supported { get; set; }
        public string type { get; set; }
    }

    public class GlobalTag
    {
        public int num_entries { get; set; }
    }

    public class Properties
    {
        public int container_type { get; set; }
        public DateTime date_local { get; set; }
        public DateTime date_utc { get; set; }
        public long duration { get; set; }
        public bool is_providing_timestamps { get; set; }
        public string muxing_application { get; set; }
        public string segment_uid { get; set; }
        public string title { get; set; }
        public string writing_application { get; set; }
        public string codec_id { get; set; }
        public string codec_private_data { get; set; }
        public int codec_private_length { get; set; }
        public int default_duration { get; set; }
        public bool default_track { get; set; }
        public string display_dimensions { get; set; }
        public int display_unit { get; set; }
        public bool enabled_track { get; set; }
        public bool forced_track { get; set; }
        public string language { get; set; }
        public BigInteger minimum_timestamp { get; set; }
        public int number { get; set; }
        public string packetizer { get; set; }
        public string pixel_dimensions { get; set; }
        public object uid { get; set; }
        public int? audio_channels { get; set; }
        public int? audio_sampling_frequency { get; set; }
        public string content_encoding_algorithms { get; set; }
    }

    public class Root
    {
        public List<object> attachments { get; set; }
        public List<Chapter> chapters { get; set; }
        public Container container { get; set; }
        public List<object> errors { get; set; }
        public string file_name { get; set; }
        public List<GlobalTag> global_tags { get; set; }
        public int identification_format_version { get; set; }
        public List<object> track_tags { get; set; }
        public List<Track> tracks { get; set; }
        public List<object> warnings { get; set; }
    }

    public class Track
    {
        public string codec { get; set; }
        public int id { get; set; }
        public Properties properties { get; set; }
        public string type { get; set; }
    }


}