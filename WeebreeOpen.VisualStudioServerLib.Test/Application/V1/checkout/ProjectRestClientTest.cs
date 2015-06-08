namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Test.Properties;

    [TestClass]
    public class ProjectRestClientTest : VsoTestBase
    {
        private IVsoProject client;

        [TestMethod]
        public void TestGetProjects()
        {
            var projects = this.client.GetTeamProjects().Result;

            var project = this.client.GetTeamProject(projects[0].Id.ToString(), true).Result;
            project.Description = DateTime.Now.Ticks.ToString();

            project = this.client.UpdateTeamProject(project).Result;
        }

        [TestMethod]
        public void TestGetTeams()
        {
            var teams = this.client.GetProjectTeams(Settings.Default.ProjectName).Result;

            var team = this.client.GetProjectTeam(Settings.Default.ProjectName, teams[0].Id.ToString()).Result;
            var teamMembers = this.client.GetTeamMembers(Settings.Default.ProjectName, team.Name).Result;
        }

        protected override void OnInitialize(VsoClient vsoClient)
        {
            this.client = vsoClient.GetService<IVsoProject>();
        }
    }
}