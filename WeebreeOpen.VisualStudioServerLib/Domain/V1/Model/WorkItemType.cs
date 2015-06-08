namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public enum FieldType
    {
        boolean,
        dateTime,
        @double,
        history,
        html,
        integer,
        plainText,
        @string,
        treePath
    }

    [DebuggerDisplay("{ReferenceName}")]
    public class Field : BaseObject
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "readOnly")]
        public bool ReadOnly { get; set; }

        [JsonProperty(PropertyName = "referenceName")]
        public string ReferenceName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "type")]
        public FieldType Type { get; set; }
    }

    [DebuggerDisplay("{Field.ReferenceName}")]
    public class FieldInstance
    {
        [JsonProperty(PropertyName = "field")]
        public Field Field { get; set; }

        [JsonProperty(PropertyName = "helpText")]
        public string HelpText { get; set; }
    }

    public class TypeLink
    {
        [JsonProperty(PropertyName = "fields")]
        public ObjectLink FieldsReference { get; set; }

        [JsonProperty(PropertyName = "workItemType")]
        public ObjectLink WorkItemTypeReference { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class WorkItemType : BaseObject
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "fieldInstances")]
        public List<FieldInstance> Fields { get; set; }

        [JsonProperty(PropertyName = "xmlForm")]
        public string Form { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class WorkItemTypeCategory : BaseObject
    {
        [JsonProperty(PropertyName = "defaultWorkItemType")]
        public WorkItemType DefaultWorkItemType { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "referenceName")]
        public string ReferenceName { get; set; }

        [JsonProperty(PropertyName = "workItemTypes")]
        public List<WorkItemType> workItemTypes { get; set; }
    }

    public class WorkItemTypeDefaults : BaseObject<TypeLink>
    {
        [JsonProperty(PropertyName = "fields")]
        public dynamic Fields { get; set; }
    }
}