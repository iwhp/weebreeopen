namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using System.Diagnostics;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;

    [DebuggerDisplay("{Name}")]
    public class Tag : ObjectWithId<string>
    {
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}