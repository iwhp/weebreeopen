namespace WeebreeOpen.Office365Api.Infrastructure.Office365Api.Mapping
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using WeebreeOpen.Office365Api.Domain.Mail;

    [JsonObject("Rootobject")]
    class FoldersMapping
    {
        public string odatacontext { get; set; }
        public Folder[] value { get; set; }
    }
}
