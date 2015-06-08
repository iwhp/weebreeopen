namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System;
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public interface IVsoWit
    {
        Task<Query> CreateQuery(string projectName, string parentPath, string queryName, string queryText);

        Task<Query> CreateQueryFolder(string projectName, string parentPath, string folderName);

        Task<WorkItem> CreateWorkItem(string projectName, string workItemTypeName, WorkItem workItem);

        Task<string> DeleteQuery(string projectName, Query query);

        Task<string> DeleteQuery(string projectName, string queryPath);

        Task<string> DownloadAttachment(string attachmentId);

        Task<ClassificationNode> GetAreaNode(string projectName, int? depth = null);

        Task<ClassificationNode> GetAreaNode(string projectName, string nodePath, int? depth = null);

        Task<JsonCollection<ClassificationNode>> GetClassificationNodes(string projectName, int? depth = null);

        Task<Field> GetField(string fieldName);

        Task<JsonCollection<Field>> GetFields();

        Task<ClassificationNode> GetIterationNode(string projectName, int? depth = null);

        Task<ClassificationNode> GetIterationNode(string projectName, string nodePath, int? depth = null);

        Task<JsonCollection<Query>> GetQueries(string projectName, string folderPath = null, int? depth = null, QueryExpandOptions options = QueryExpandOptions.none, bool? includeDeleted = null);

        Task<Query> GetQuery(string projectName, string folderPathOrId, int? depth = null, QueryExpandOptions options = QueryExpandOptions.none, bool? includeDeleted = null);

        Task<WorkItem> GetWorkItem(int workItemId, RevisionExpandOptions options = RevisionExpandOptions.none);

        Task<JsonCollection<HistoryComment>> GetWorkItemHistory(int workitemId, int? top = null, int? skip = null);

        Task<WorkItemRelationType> GetWorkItemRelationType(string relationName);

        Task<JsonCollection<WorkItemRelationType>> GetWorkItemRelationTypes();

        Task<WorkItem> GetWorkItemRevision(int workItemId, int revision, RevisionExpandOptions options = RevisionExpandOptions.none);

        Task<HistoryComment> GetWorkItemRevisionHistory(int workitemId, int revision);

        Task<JsonCollection<WorkItem>> GetWorkItemRevisions(int workItemId, int? top = null, int? skip = null, RevisionExpandOptions options = RevisionExpandOptions.none);

        Task<JsonCollection<WorkItem>> GetWorkItems(int[] workItemIds, RevisionExpandOptions options = RevisionExpandOptions.none, DateTime? asOfDate = null, string[] fields = null);

        Task<WorkItemType> GetWorkItemType(string projectName, string typeName);

        Task<JsonCollection<WorkItemTypeCategory>> GetWorkItemTypeCategories(string projectName);

        Task<WorkItemTypeCategory> GetWorkItemTypeCategory(string projectName, string categoryName);

        Task<WorkItemTypeDefaults> GetWorkItemTypeDefaultValues(string projectName, string workItemTypeName);

        Task<JsonCollection<WorkItemType>> GetWorkItemTypes(string projectName);

        Task<WorkItemUpdate> GetWorkItemUpdate(int workItemId, int revisionId);

        Task<JsonCollection<WorkItemUpdate>> GetWorkItemUpdates(int workItemId, int? top = null, int? skip = null);

        Task<Query> MoveQuery(string projectName, string newPath, Query query);

        Task<FlatQueryResult> RunFlatQuery(string projectName, string queryText);

        Task<FlatQueryResult> RunFlatQuery(string projectName, Query query);

        Task<LinkQueryResult> RunLinkQuery(string projectName, string queryText);

        Task<LinkQueryResult> RunLinkQuery(string projectName, Query query);

        Task<Query> UndeleteQuery(string projectName, Query query, bool? undeleteDescendants = null);

        Task<Query> UpdateQuery(string projectName, Query query);

        Task<WorkItem> UpdateWorkItem(WorkItem workItem);

        Task<ObjectWithId<string>> UploadAttachment(string projectName, string area, string fileName, byte[] content);

        Task<ObjectWithId<string>> UploadAttachment(string fileName, string content);
    }
}