namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System.Diagnostics;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;

    [DebuggerDisplay("{Self.Href}")]
    public class SelfLink
    {
        [JsonProperty(PropertyName = "self")]
        public ObjectLink Self { get; set; }
    }
}