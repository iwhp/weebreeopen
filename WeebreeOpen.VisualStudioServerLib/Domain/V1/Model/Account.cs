namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public class Account
    {
        [JsonProperty(PropertyName = "accountId")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "accountName")]
        public string AccountName { get; set; }

        [JsonProperty(PropertyName = "accountOwner")]
        public string AccountOwner { get; set; }

        [JsonProperty(PropertyName = "accountStatus")]
        public string AccountStatus { get; set; }

        [JsonProperty(PropertyName = "accountType")]
        public string AccountType { get; set; }

        [JsonProperty(PropertyName = "accountUri")]
        public string AccountUri { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdDate")]
        public string CreatedDate { get; set; }

        [JsonProperty(PropertyName = "lastUpdatedBy")]
        public string LastUpdatedBy { get; set; }

        [JsonProperty(PropertyName = "lastUpdatedDate")]
        public string LastUpdatedDate { get; set; }

        [JsonProperty(PropertyName = "organizationName")]
        public string OrganizationName { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public Properties Properties { get; set; }
    }

    public class Properties
    {
    }
}