namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("From")]
    public class MessageFrom
    {
        public EmailAddress EmailAddress { get; set; }
    }
}
