﻿namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("Torecipient")]
    public class MessageToRecipient
    {
        public EmailAddress EmailAddress { get; set; }
    }
}
