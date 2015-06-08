namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;

    [TestClass]
    public class ProjectCollectionRestClientTest : VsoTestBase
    {
        private IVsoProjectCollection client;

        [TestMethod]
        public void TestGetProjectCollections()
        {
            var collections = this.client.GetProjectCollections().Result;
            var collection = this.client.GetProjectCollection(collections[0].Name).Result;
        }

        protected override void OnInitialize(VsoClient vsoClient)
        {
            this.client = vsoClient.GetService<IVsoProjectCollection>();
        }
    }
}