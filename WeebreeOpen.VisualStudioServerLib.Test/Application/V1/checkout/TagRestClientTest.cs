namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Test.Properties;

    [TestClass]
    public class TagRestClientTest : VsoTestBase
    {
        private IVsoTag client;

        [TestMethod]
        public void TestCreateAndUpdateTag()
        {
            var newTag = this.client.CreateTag(Settings.Default.ProjectId, "TestTag").Result;

            newTag.Name = "TestTagRenamed";
            newTag = this.client.UpdateTag(Settings.Default.ProjectId, newTag).Result;

            newTag = this.client.GetTag(Settings.Default.ProjectId, "TestTagRenamed").Result;

            var response = this.client.DeleteTag(Settings.Default.ProjectId, newTag).Result;
        }

        [TestMethod]
        public void TestGetTagList()
        {
            var tags = this.client.GetTagList(Settings.Default.ProjectId).Result;
        }

        protected override void OnInitialize(VsoClient vsoClient)
        {
            this.client = vsoClient.GetService<IVsoTag>();
        }
    }
}