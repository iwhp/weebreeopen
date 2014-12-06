namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("Body")]
    public class MessageBody
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }
}
