namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class VsoException : Exception
    {
        public VsoException()
        {
        }

        public VsoException(string errorMessage)
            : base(errorMessage)
        {
        }

        [JsonProperty(PropertyName = "$id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "innerException")]
        public object ServerInnerException { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string ErrorMessage { get; set; }

        public override string Message
        {
            get
            {
                return this.ErrorMessage;
            }
        }

        [JsonProperty(PropertyName = "typeName")]
        public string TypeName { get; set; }

        [JsonProperty(PropertyName = "typeKey")]
        public string TypeKey { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty(PropertyName = "eventId")]
        public int EventId { get; set; }
    }
}