namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System.Diagnostics;
    using Newtonsoft.Json;

    [DebuggerDisplay("{Url}")]
    public abstract class BaseObject
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as BaseObject;
            if (other == null)
            {
                return false;
            }

            return this.Url == other.Url;
        }

        public override int GetHashCode()
        {
            return this.Url != null ? this.Url.GetHashCode() : base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Url != null ? this.Url.ToString() : base.ToString();
        }
    }
}