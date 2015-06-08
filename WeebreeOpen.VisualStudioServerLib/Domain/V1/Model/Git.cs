namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    [DebuggerDisplay("{CommitId}")]
    public class Commit : BaseObject
    {
        [JsonProperty("author")]
        public GitUser Author { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("commitId")]
        public string CommitId { get; set; }

        [JsonProperty("committer")]
        public GitUser Committer { get; set; }

        [JsonProperty("parents")]
        public IList<string> Parents { get; set; }

        [JsonProperty("treeId")]
        public string TreeId { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class GitBranchInfo
    {
        [JsonProperty("aheadCount")]
        public int AheadCount { get; set; }

        [JsonProperty("behindCount")]
        public int BehindCount { get; set; }

        [JsonProperty("commit")]
        public Commit Commit { get; set; }

        [JsonProperty("isBaseVersion")]
        public bool IsBaseVersion { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class GitReference : BaseObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class GitUser
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class Repository : ObjectWithId<string>
    {
        [JsonProperty("defaultBranch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("project")]
        public TeamProject Project { get; set; }

        [JsonProperty("remoteUrl")]
        public string RemoteUrl { get; set; }
    }
}