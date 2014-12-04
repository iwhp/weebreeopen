namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("Ccrecipient")]
    public class MessageCcRecipient
    {
        public EmailAddress EmailAddress { get; set; }
    }
}