namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("Emailaddress")]
    public class EmailAddress
    {
        public string Address { get; set; }

        public string Name { get; set; }
    }
}