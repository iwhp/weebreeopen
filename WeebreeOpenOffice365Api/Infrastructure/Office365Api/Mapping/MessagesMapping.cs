﻿namespace WeebreeOpen.Office365Api.Infrastructure.Office365Api.Mapping
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using WeebreeOpen.Office365Api.Domain.Mail;


    [JsonObject("Rootobject")]
    public class MessagesMapping
    {
        public string odatacontext { get; set; }
        public Message[] value { get; set; }
        public string odatanextLink { get; set; }
    }
}
