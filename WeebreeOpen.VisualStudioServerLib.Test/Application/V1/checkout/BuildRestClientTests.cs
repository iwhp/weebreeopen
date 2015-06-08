namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Test.Properties;

    [TestClass]
    public class BuildRestClientTests : VsoTestBase
    {
        private IVsoBuild client;

        [TestMethod]
        public void TestBuildQualities()
        {
            var qualities = this.client.GetBuildQualities(Settings.Default.ProjectName).Result;

            var newQuality = DateTime.Now.Ticks.ToString();
            var result = this.client.AddBuildQuality(Settings.Default.ProjectName, newQuality).Result;
            qualities = this.client.GetBuildQualities(Settings.Default.ProjectName).Result;

            result = this.client.DeleteBuildQuality(Settings.Default.ProjectName, newQuality).Result;
            qualities = this.client.GetBuildQualities(Settings.Default.ProjectName).Result;
        }

        [TestMethod]
        public void TestBuildRequests()
        {
            var requests = this.client.GetBuildRequests(Settings.Default.ProjectName).Result;
            var definitions = this.client.GetBuildDefinitions(Settings.Default.ProjectName).Result;
            var result = this.client.UpdateBuildRequest(Settings.Default.ProjectName, requests.Items[0].Id, BuildStatus.Cancelled).Result;

            var request = this.client.RequestBuild(Settings.Default.ProjectName, definitions.Items[0].Id, BuildReason.Manual, BuildPriority.Low).Result;
            result = this.client.CancelBuildRequest(Settings.Default.ProjectName, request.Id).Result;
        }

        [TestMethod]
        public void TestBuilds()
        {
            var builds = this.client.GetBuilds(Settings.Default.ProjectName).Result;

            if (builds.Items.Count > 0)
            {
                var build = this.client.GetBuild(Settings.Default.ProjectName, builds.Items[0].Id, BuildDetails.BuildMessage, BuildDetails.GetStatus).Result;

                build = this.client.UpdateBuild(Settings.Default.ProjectName, build.Id, BuildStatus.Cancelled).Result;
                var result = this.client.DeleteBuild(Settings.Default.ProjectName, build.Id).Result;
                builds = this.client.GetBuilds(Settings.Default.ProjectName).Result;
            }
        }

        [TestMethod]
        public void TestGetBuildDefinitions()
        {
            var definitions = this.client.GetBuildDefinitions(Settings.Default.ProjectName).Result;
            var definition = this.client.GetBuildDefinition(Settings.Default.ProjectName, definitions.Items[0].Id).Result;
        }

        [TestMethod]
        public void TestGetBuildQueues()
        {
            var queues = this.client.GetBuildQueues().Result;
            var queue = this.client.GetBuildQueue(queues.Items[0].Id).Result;
        }

        protected override void OnInitialize(VsoClient vsoClient)
        {
            this.client = vsoClient.GetService<IVsoBuild>();
        }
    }
}