namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;

    [TestClass]
    public class VersionControlRestClientTest : VsoTestBase
    {
        private IVsoVersionControl client;

        [TestMethod]
        public void TestGetBranches()
        {
            var rootBranches = this.client.GetRootBranches().Result;
            var branch = this.client.GetBranch(rootBranches[1].Path).Result;
        }

        [TestMethod]
        public void TestGetChangesets()
        {
            var changesets = this.client.GetChangesets().Result;
            var changesetBatch = this.client.GetChangesets(new int[]
            {
                changesets[0].Id,
                changesets[1].Id
            }).Result;

            var changeset = this.client.GetChangeset(changesets[0].Id).Result;
            var change = this.client.GetChangesetChanges(changeset.Id).Result;
            var workitems = this.client.GetChangesetWorkItems(changeset.Id).Result;
        }

        [TestMethod]
        public void TestGetLabels()
        {
            var labels = this.client.GetLabels().Result;
            var label = this.client.GetLabel(labels[0].Id.ToString()).Result;
            var items = this.client.GetLabelledItems(label.Id.ToString()).Result;
        }

        [TestMethod]
        public void TestGetShelvesets()
        {
            var shelvesets = this.client.GetShelvesets().Result;
            var shelveset = this.client.GetShelveset(shelvesets[0].Id, true, true).Result;

            var changes = this.client.GetShelvesetChanges(shelveset.Id).Result;
            var workItems = this.client.GetShelvesetWorkItems(shelveset.Id).Result;
        }

        [TestMethod]
        public void TestGetVersionControlItems()
        {
            var changesets = this.client.GetChangesets().Result;

            if (changesets.Count > 1)
            {
                var change = this.client.GetChangesetChanges(changesets[1].Id).Result;

                var vcItemContent = this.client.GetVersionControlItemContent(new VersionSearchFilter() { Path = change[0].Item.Path }).Result;
                var vcItem = this.client.GetVersionControlItem(new VersionSearchFilter() { Path = change[0].Item.Path }).Result;

                var vcVersions = this.client.GetVersionControlItemVersions(new VersionSearchFilter() { Path = change[0].Item.Path }).Result;

                if (change.Count > 1)
                {
                    var multipleVcVersions = this.client.GetVersionControlItemVersions(
                        new List<VersionSearchFilter>(){ new VersionSearchFilter() { Path = change[0].Item.Path, Type = VersionType.Tip }, new VersionSearchFilter() { Path = change[1].Item.Path } }).Result;
                }
            }
        }

        protected override void OnInitialize(VsoClient vsoClient)
        {
            this.client = vsoClient.GetService<IVsoVersionControl>();
        }
    }
}