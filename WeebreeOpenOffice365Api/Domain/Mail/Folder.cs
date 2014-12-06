namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("Value")]
    public class Folder
    {
        public string odataid { get; set; }
        public string Id { get; set; }
        public string ParentFolderId { get; set; }
        public string DisplayName { get; set; }
        public int ChildFolderCount { get; set; }
    }
}
