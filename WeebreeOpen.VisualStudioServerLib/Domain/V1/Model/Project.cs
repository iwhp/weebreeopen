namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public enum ProjectState
    {
        WellFormed,
        CreatePending,
        Deleting,
        New,
        All
    }

    public class Capabilities
    {
        [JsonProperty(PropertyName = "processTemplate")]
        public ProcessTemplate ProcessTemplate { get; set; }

        [JsonProperty(PropertyName = "versioncontrol")]
        public Versioncontrol VersionControl { get; set; }
    }

    public class ProcessTemplate
    {
        [JsonProperty(PropertyName = "templateName")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class Team : ObjectWithId<string>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class TeamProject : ObjectWithId<Guid, TeamProjectLink>
    {
        [JsonProperty(PropertyName = "capabilities")]
        public Capabilities Capabilities { get; set; }

        [JsonProperty(PropertyName = "defaultTeam")]
        public Team DefaultTeam { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectState State { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class TeamProjectCollection : ObjectWithId<string, TeamProjectCollectionLink>
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }

    public class TeamProjectCollectionLink : ObjectLink
    {
        [JsonProperty(PropertyName = "web")]
        public ObjectLink Web { get; set; }
    }

    public class TeamProjectLink : ObjectLink
    {
        [JsonProperty(PropertyName = "collection")]
        public ObjectLink Collection { get; set; }

        [JsonProperty(PropertyName = "web")]
        public ObjectLink Web { get; set; }
    }

    public class Versioncontrol
    {
        [JsonProperty(PropertyName = "sourceControlType")]
        public string Type { get; set; }
    }
}