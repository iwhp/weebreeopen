namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System.Diagnostics;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;

    [DebuggerDisplay("{Id}")]
    public class ObjectWithId<TId, TLinks> : BaseObject<TLinks>
    {
        [JsonProperty(PropertyName = "id")]
        public TId Id { get; set; }
    }
}