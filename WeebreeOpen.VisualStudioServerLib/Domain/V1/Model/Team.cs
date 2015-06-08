namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;

    [DebuggerDisplay("{Name}")]
    public class ProjectTeam : ObjectWithId<Guid>
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "identityUrl")]
        public string IdentityUrl { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{DisplayName}")]
    public class UserIdentity : ObjectWithId<Guid>
    {
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "isContainer")]
        public bool? IsContainer { get; set; }

        [JsonProperty(PropertyName = "uniqueName")]
        public string UniqueName { get; set; }
    }
}