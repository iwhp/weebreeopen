namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure;

    /// <summary>
    /// Account REST API client
    /// </summary>
    public class AccountRestClient : RestClientVersion1, IVsoAccount
    {
        public AccountRestClient(string authToken)
            : base(VsspsRootUrl, new OAuthFilter(authToken))
        {
        }

        private const string VsspsRootUrl = "https://app.vssps.visualstudio.com";

        protected override string SubSystemName
        {
            get
            {
                return "accounts";
            }
        }

        /// <summary>
        /// Get specific account
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Account> GetAccount(string name)
        {
            string response = await this.GetResponse(string.Format("/{0}", name));
            return JsonConvert.DeserializeObject<Account>(response);
        }

        /// <summary>
        /// Get account list for current user
        /// </summary>
        /// <returns></returns>
        public async Task<JsonCollection<Account>> GetAccountList()
        {
            string response = await this.GetResponse(string.Empty);
            return JsonConvert.DeserializeObject<JsonCollection<Account>>(response);
        }
    }
}