namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Newtonsoft.Json;

    [DebuggerDisplay("{Count}")]
    public class JsonCollection<T> where T : class
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<T> Items { get; set; }

        public T this[int index]
        {
            get
            {
                return this.Items[index];
            }
            set
            {
                this.Items[index] = value;
            }
        }
    }
}