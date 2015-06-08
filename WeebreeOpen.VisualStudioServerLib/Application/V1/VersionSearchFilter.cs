namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;

    public struct VersionSearchFilter
    {
        [JsonProperty(PropertyName = "versionOptions", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public VersionOptions? Options;
        [JsonProperty(PropertyName = "path")]
        public string Path;
        [JsonProperty(PropertyName = "recursionLevel", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public RecursionLevel? Recursion;
        [JsonProperty(PropertyName = "versionType", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public VersionType? Type;
        [JsonProperty(PropertyName = "version")]
        public string Value;
    }
}