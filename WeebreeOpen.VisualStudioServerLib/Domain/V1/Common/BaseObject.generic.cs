namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;

    public abstract class BaseObject<T> : BaseObject
    {
        [JsonProperty(PropertyName = "_links")]
        public T ObjectLinks { get; set; }
    }
}