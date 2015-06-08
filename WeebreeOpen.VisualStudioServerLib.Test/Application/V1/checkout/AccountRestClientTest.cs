namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Test.Properties;

    [TestClass]
    public class AccountRestClientTest
    {
        private IVsoAccount client;

        [TestInitialize]
        public void AccountRestClient_Initialize()
        {
            this.client = new AccountRestClient(Settings.Default.AccessToken);
        }

        [TestMethod]
        public void AccountRestClient_GetAccount()
        {
            var account = this.client.GetAccount(Settings.Default.VsoTenantName).Result;
        }

        [TestMethod]
        public void AccountRestClient_GetAccountList()
        {
            var accounts = this.client.GetAccountList().Result;
        }
    }
}