namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Test.Properties;

    [TestClass]
    public class WitRestClientTest : VsoTestBase
    {
        private IVsoWit client;

        [TestMethod]
        public void TestCreateAndUpdateQueries()
        {
            var rootQueries = this.client.GetQueries(Settings.Default.ProjectName).Result;
            var sharedQueries = this.client.GetQuery(Settings.Default.ProjectName, "Shared Queries", 2, QueryExpandOptions.all).Result;

            var newQuery = this.client.CreateQuery(Settings.Default.ProjectName,
                "Shared Queries/Troubleshooting",
                string.Format("REST {0}", DateTime.Now.Ticks),
                "select System.Id from Issue").Result;

            newQuery.Name = string.Format("{0}_Renamed", newQuery.Name);
            newQuery.Wiql = "select System.Id, System.AssignedTo from Issue";
            newQuery = this.client.UpdateQuery(Settings.Default.ProjectName, newQuery).Result;

            string response = this.client.DeleteQuery(Settings.Default.ProjectName, newQuery).Result;
            newQuery = this.client.GetQuery(Settings.Default.ProjectName, newQuery.Id, null, QueryExpandOptions.all, true).Result;
            newQuery = this.client.UndeleteQuery(Settings.Default.ProjectName, newQuery).Result;
            response = this.client.DeleteQuery(Settings.Default.ProjectName, newQuery).Result;

            var newFolder = this.client.CreateQueryFolder(Settings.Default.ProjectName, "Shared Queries", string.Format("REST {0}", DateTime.Now.Ticks)).Result;

            newFolder = this.client.MoveQuery(Settings.Default.ProjectName, "Shared Queries/Troubleshooting", newFolder).Result;

            response = this.client.DeleteQuery(Settings.Default.ProjectName, newFolder).Result;
        }

        [TestMethod]
        public void TestCreateAndUpdateWorkItem()
        {
            var defaultValues = this.client.GetWorkItemTypeDefaultValues(Settings.Default.ProjectName, "Bug").Result;
            var workItems = this.client.GetWorkItems(new int[] { Settings.Default.WorkItemId }, RevisionExpandOptions.all).Result;

            // Create new work item
            var bug = new WorkItem();
            bug.Fields["System.Title"] = "Test bug 1";
            bug.Fields["System.History"] = DateTime.Now.ToString();
            bug = this.client.CreateWorkItem(Settings.Default.ProjectName, "Bug", bug).Result;

            var other = workItems[0];

            // Update fields, add a link
            bug.Fields["System.Title"] = string.Format("{0} (updated)", bug.Fields["System.Title"]);
            bug.Fields["System.Tags"] = "SimpleTag";

            bug.Relations.Add(new WorkItemRelation()
            {
                Url = other.Url,
                Rel = "System.LinkTypes.Dependency-Forward",
                Attributes = new RelationAttributes() { Comment = "Hello world" }
            });

            bug = this.client.UpdateWorkItem(bug).Result;

            //TODO: update link
            //bug.Relations[0].Attributes.IsLocked = true;
            //bug = _client.UpdateWorkItem(bug).Result;

            // Remove link
            bug.Relations.RemoveAt(0);
            bug = this.client.UpdateWorkItem(bug).Result;

            // Add hyperlink
            bug.Relations.Add(new WorkItemRelation()
            {
                Url = "http://www.bing.com",
                Rel = "Hyperlink",
                Attributes = new RelationAttributes() { Comment = "Hello world" }
            });

            bug = this.client.UpdateWorkItem(bug).Result;

            // Remove it
            bug.Relations.RemoveAt(0);
            bug = this.client.UpdateWorkItem(bug).Result;

            // Get all revisions
            var revisions = this.vsoClient.Get<JsonCollection<WorkItem>>(bug.References.Revisions.Href).Result;
        }

        [TestMethod]
        public void TestGetClassificationNodes()
        {
            var nodes = this.client.GetClassificationNodes(Settings.Default.ProjectName).Result;

            var rootArea = this.client.GetAreaNode(Settings.Default.ProjectName, 5).Result;
            var rootIteration = this.client.GetIterationNode(Settings.Default.ProjectName, 5).Result;

            var iteration1 = this.client.GetIterationNode(Settings.Default.ProjectName, "Iteration 1").Result;
            var area1 = this.client.GetAreaNode(Settings.Default.ProjectName, "Area 1").Result;
        }

        [TestMethod]
        public void TestGetFields()
        {
            var fields = this.client.GetFields().Result;
            var field = this.client.GetField(fields.Items[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetRelationTypes()
        {
            var relations = this.client.GetWorkItemRelationTypes().Result;
            var relation = this.client.GetWorkItemRelationType(relations.Items[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetWorkItemHistory()
        {
            var history = this.client.GetWorkItemHistory(Settings.Default.WorkItemId).Result;
            var revHistory = this.client.GetWorkItemRevisionHistory(Settings.Default.WorkItemId, Settings.Default.WorkItemRevision).Result;
        }

        [TestMethod]
        public void TestGetWorkItemRevisions()
        {
            var revisions = this.client.GetWorkItemRevisions(Settings.Default.WorkItemId, null, null, RevisionExpandOptions.all).Result;
            var revision = this.client.GetWorkItemRevision(Settings.Default.WorkItemId, Settings.Default.WorkItemRevision).Result;

            var areaPath = revision.Fields["System.AreaPath"];
        }

        [TestMethod]
        public void TestGetWorkItemTypeCategories()
        {
            var workItemTypeCategories = this.client.GetWorkItemTypeCategories(Settings.Default.ProjectName).Result;
            var workItemTypeCategory = this.client.GetWorkItemTypeCategory(Settings.Default.ProjectName, workItemTypeCategories.Items[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetWorkItemTypes()
        {
            var workItemTypes = this.client.GetWorkItemTypes(Settings.Default.ProjectName).Result;
            var workItemType = this.client.GetWorkItemType(Settings.Default.ProjectName, workItemTypes.Items[0].Name).Result;
        }

        [TestMethod]
        public void TestGetWorkItemUpdates()
        {
            var updates = this.client.GetWorkItemUpdates(Settings.Default.WorkItemId).Result;
            var update = this.client.GetWorkItemUpdate(Settings.Default.WorkItemId, Settings.Default.WorkItemRevision).Result;
        }

        [TestMethod]
        public void TestRunQueries()
        {
            const string FLAT_QUERY = "select System.Id, System.AssignedTo from Issue";
            var flatResult = this.client.RunFlatQuery(Settings.Default.ProjectName, FLAT_QUERY).Result;

            // Run one hop query
            var linkResult = this.client.RunLinkQuery(Settings.Default.ProjectName, "select System.Id from WorkItemLinks where ([Source].[System.WorkItemType] <> '' and [Source].[System.State] <> '') and ([System.Links.LinkType] <> '') and ([Target].[System.WorkItemType]<> '') order by System.Id mode(MayContain)").Result;

            // Run tree query
            var treeResult = this.client.RunLinkQuery(Settings.Default.ProjectName, "select System.Id from WorkItemLinks where ([Source].[System.WorkItemType] <> '' and [Source].[System.State] <> '') and ([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') and ([Target].[System.WorkItemType] <> '') order by System.Id mode(Recursive)").Result;

            var newQuery = this.client.CreateQuery(Settings.Default.ProjectName,
                "Shared Queries/Troubleshooting",
                string.Format("REST {0}", DateTime.Now.Ticks),
                FLAT_QUERY).Result;

            var result = this.client.RunFlatQuery(Settings.Default.ProjectName, newQuery).Result;

            this.client.DeleteQuery(Settings.Default.ProjectName, newQuery).Wait();
        }

        [TestMethod]
        public void TestUploadDownloadAttachments()
        {
            var fileRef = this.client.UploadAttachment("Test.txt", "Hello world").Result;
            string content = this.client.DownloadAttachment(fileRef.Id).Result;

            var bug = this.client.GetWorkItem(Settings.Default.WorkItemId, RevisionExpandOptions.relations).Result;

            // Add attachment to WI
            bug.Relations.Add(new WorkItemRelation()
            {
                Url = fileRef.Url,
                Rel = "AttachedFile",
                Attributes = new RelationAttributes() { Comment = "Hello world" }
            });

            bug = this.client.UpdateWorkItem(bug).Result;

            // Remove attachment from WI
            bug.Relations.RemoveAt(2);
            bug = this.client.UpdateWorkItem(bug).Result;
        }

        protected override void OnInitialize(VsoClient vsoClient)
        {
            this.client = vsoClient.GetService<IVsoWit>();
        }
    }
}