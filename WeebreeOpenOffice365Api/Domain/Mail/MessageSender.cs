namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("Sender")]
    public class MessageSender
    {
        public EmailAddress EmailAddress { get; set; }
    }
}