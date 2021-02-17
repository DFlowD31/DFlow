﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using DFlow.Classes;
//
//    var tmdb = Tmdb.FromJson(jsonString);

namespace DFlow.Classes
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TMDB_movie
    {
        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public long? Page { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<Result> Results { get; set; }

        [JsonProperty("total_pages", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalPages { get; set; }

        [JsonProperty("total_results", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalResults { get; set; }

        public partial class Result
        {
            [JsonProperty("adult", NullValueHandling = NullValueHandling.Ignore)]
            public bool? Adult { get; set; }

            [JsonProperty("backdrop_path", NullValueHandling = NullValueHandling.Ignore)]
            public string BackdropPath { get; set; }

            [JsonProperty("genre_ids", NullValueHandling = NullValueHandling.Ignore)]
            public List<long> GenreIds { get; set; }

            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public long? Id { get; set; }

            [JsonProperty("original_language", NullValueHandling = NullValueHandling.Ignore)]
            public string OriginalLanguage { get; set; }

            [JsonProperty("original_title", NullValueHandling = NullValueHandling.Ignore)]
            public string OriginalTitle { get; set; }

            [JsonProperty("overview", NullValueHandling = NullValueHandling.Ignore)]
            public string Overview { get; set; }

            [JsonProperty("popularity", NullValueHandling = NullValueHandling.Ignore)]
            public double? Popularity { get; set; }

            [JsonProperty("poster_path", NullValueHandling = NullValueHandling.Ignore)]
            public string PosterPath { get; set; }

            [JsonProperty("release_date", NullValueHandling = NullValueHandling.Ignore)]
            public DateTimeOffset? ReleaseDate { get; set; }

            [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
            public string Title { get; set; }

            [JsonProperty("video", NullValueHandling = NullValueHandling.Ignore)]
            public bool? Video { get; set; }

            [JsonProperty("vote_average", NullValueHandling = NullValueHandling.Ignore)]
            public double? VoteAverage { get; set; }

            [JsonProperty("vote_count", NullValueHandling = NullValueHandling.Ignore)]
            public long? VoteCount { get; set; }
        }
    }

    public partial class TMDB_movie
    {
        public static TMDB_movie FromJson(string json) => JsonConvert.DeserializeObject<TMDB_movie>(json, DFlow.Classes.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TMDB_movie self) => JsonConvert.SerializeObject(self, DFlow.Classes.Converter.Settings);
        public static string ToJson(this TMDB_collection self) => JsonConvert.SerializeObject(self, DFlow.Classes.Converter.Settings);
        public static string ToJson(this TMDB_tv self) => JsonConvert.SerializeObject(self, DFlow.Classes.Converter.Settings);
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