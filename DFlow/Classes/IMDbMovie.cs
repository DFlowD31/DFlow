namespace DFlow
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ImDbMovie
    {
        [JsonProperty("d", NullValueHandling = NullValueHandling.Ignore)]
        public List<D> D { get; set; }

        [JsonProperty("q", NullValueHandling = NullValueHandling.Ignore)]
        public string Q { get; set; }

        [JsonProperty("v", NullValueHandling = NullValueHandling.Ignore)]
        public long? V { get; set; }
    }

    public partial class D
    {
        [JsonProperty("i", NullValueHandling = NullValueHandling.Ignore)]
        public I I { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("l", NullValueHandling = NullValueHandling.Ignore)]
        public string L { get; set; }

        [JsonProperty("q", NullValueHandling = NullValueHandling.Ignore)]
        public string Q { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rank { get; set; }

        [JsonProperty("s", NullValueHandling = NullValueHandling.Ignore)]
        public string S { get; set; }

        [JsonProperty("v", NullValueHandling = NullValueHandling.Ignore)]
        public List<V> V { get; set; }

        [JsonProperty("vt", NullValueHandling = NullValueHandling.Ignore)]
        public long? Vt { get; set; }

        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        public long? Y { get; set; }

        [JsonProperty("yr", NullValueHandling = NullValueHandling.Ignore)]
        public string Yr { get; set; }
    }

    public partial class I
    {
        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("imageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ImageUrl { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }
    }

    public partial class V
    {
        [JsonProperty("i", NullValueHandling = NullValueHandling.Ignore)]
        public I I { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("l", NullValueHandling = NullValueHandling.Ignore)]
        public string L { get; set; }

        [JsonProperty("s", NullValueHandling = NullValueHandling.Ignore)]
        public string S { get; set; }
    }

    public partial class ImDbMovie
    {
        public static ImDbMovie FromJson(string json) => JsonConvert.DeserializeObject<ImDbMovie>(json, DFlow.Classes.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ImDbMovie self) => JsonConvert.SerializeObject(self, DFlow.Classes.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}