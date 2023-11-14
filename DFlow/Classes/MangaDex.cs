using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPortal
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AltTitle
    {
        public string en { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string ru { get; set; }
        public string id { get; set; }

        [JsonProperty("zh-hk")]
        public string zhhk { get; set; }
        public string zh { get; set; }
        public string ja { get; set; }
        public string ko { get; set; }
        public string ar { get; set; }
        public string sq { get; set; }
        public string ka { get; set; }

        [JsonProperty("ja-ro")]
        public string jaro { get; set; }
        public string vi { get; set; }
        public string kk { get; set; }
    }

    public class Attributes
    {
        public Title title { get; set; }
        public List<AltTitle> altTitles { get; set; }
        public Description description { get; set; }
        public bool isLocked { get; set; }
        public Links links { get; set; }
        public string originalLanguage { get; set; }
        public string lastVolume { get; set; }
        public string lastChapter { get; set; }
        public string publicationDemographic { get; set; }
        public string status { get; set; }
        public int? year { get; set; }
        public string contentRating { get; set; }
        public List<Tag> tags { get; set; }
        public string state { get; set; }
        public bool chapterNumbersResetOnNewVolume { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int version { get; set; }
        public List<string> availableTranslatedLanguages { get; set; }
        public string latestUploadedChapter { get; set; }
        public Name name { get; set; }
        public string group { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string type { get; set; }
        public Attributes attributes { get; set; }
        public List<Relationship> relationships { get; set; }
    }

    public class Description
    {
        public string en { get; set; }
        public string ru { get; set; }
        public string kk { get; set; }
    }

    public class Links
    {
        public string al { get; set; }
        public string ap { get; set; }
        public string bw { get; set; }
        public string kt { get; set; }
        public string mu { get; set; }
        public string amz { get; set; }
        public string cdj { get; set; }
        public string ebj { get; set; }
        public string mal { get; set; }
        public string engtl { get; set; }
        public string raw { get; set; }
    }

    public class Name
    {
        public string en { get; set; }
    }

    public class Relationship
    {
        public string id { get; set; }
        public string type { get; set; }
        public string related { get; set; }
    }

    public class MangaDex
    {
        public string result { get; set; }
        public string response { get; set; }
        public List<Datum> data { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int total { get; set; }
    }

    public class Tag
    {
        public string id { get; set; }
        public string type { get; set; }
        public Attributes attributes { get; set; }
        public List<object> relationships { get; set; }
    }

    public class Title
    {
        public string en { get; set; }
    }
}