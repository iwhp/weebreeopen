namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public enum NodeType
    {
        area,
        iteration,
    }

    [DebuggerDisplay("{Name}")]
    public class ClassificationNode : BaseObject<SelfLink>
    {
        [JsonProperty(PropertyName = "children")]
        public List<ClassificationNode> Children { get; set; }

        [JsonProperty(PropertyName = "hasChildren")]
        public bool HasChildren { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "structureType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeType StructureType { get; set; }
    }
}