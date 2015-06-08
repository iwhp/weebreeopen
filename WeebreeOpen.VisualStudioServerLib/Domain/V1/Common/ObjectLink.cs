namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System.Diagnostics;
    using Newtonsoft.Json;

    [DebuggerDisplay("{Href}")]
    public class ObjectLink
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }
}