namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("Replyto")]
    public class MessageReplyTo
    {
        public EmailAddress EmailAddress { get; set; }
    }
}