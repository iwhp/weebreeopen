namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Test.Properties;

    [TestClass]
    public class GitRestClientTests : VsoTestBase
    {
        private IVsoGit client;

        [TestMethod]
        public void TestRepositories()
        {
            var repos = this.client.GetRepositories().Result;
            var repo = this.client.GetRepository(repos.Items[0].Id).Result;

            var stats = this.client.GetBranchStatistics(repo.Id).Result;
            var stat = this.client.GetBranchStatistics(repo.Id, "master").Result;

            var refs = this.client.GetRefs(repo.Id).Result;

            var newRepo = this.client.CreateRepository("MyRepo", Settings.Default.ProjectId).Result;
            newRepo = this.client.RenameRepository(newRepo.Id, "MyRepoRenamed").Result;
            string result = this.client.DeleteRepository(newRepo.Id).Result;
        }

        protected override void OnInitialize(VsoClient vsoClient)
        {
            this.client = vsoClient.GetService<IVsoGit>();
        }
    }
}