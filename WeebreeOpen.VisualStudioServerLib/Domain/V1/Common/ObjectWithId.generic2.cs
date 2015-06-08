namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System.Diagnostics;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;

    [DebuggerDisplay("{Id}")]
    public class ObjectWithId<T> : BaseObject
    {
        [JsonProperty(PropertyName = "id")]
        public T Id { get; set; }
    }
}