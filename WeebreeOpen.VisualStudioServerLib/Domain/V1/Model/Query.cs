namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public enum QueryType
    {
        flat,
        tree,
        oneHop
    }

    public class FlatQueryResult : QueryResult
    {
        [JsonProperty(PropertyName = "workItems")]
        public List<WorkItem> WorkItems { get; set; }
    }

    public class LinkQueryResult : QueryResult
    {
        [JsonProperty(PropertyName = "workItemRelations")]
        public List<WorkItemRelation> Relations { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class Query : ObjectWithId<string, QueryLink>
    {
        [JsonProperty(PropertyName = "children")]
        public List<Query> Children { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public List<Field> Columns { get; set; }

        [JsonProperty(PropertyName = "hasChildren")]
        public bool HasChildren { get; set; }

        [JsonProperty(PropertyName = "isFolder")]
        public bool IsFolder { get; set; }

        [JsonProperty(PropertyName = "isPublic")]
        public bool IsPublic { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "queryType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public QueryType QueryType { get; set; }

        [JsonProperty(PropertyName = "sortColumns")]
        public List<SortColumn> SortColumns { get; set; }

        [JsonProperty(PropertyName = "wiql")]
        public string Wiql { get; set; }
    }

    public class QueryLink : ObjectLink
    {
        [JsonProperty(PropertyName = "html")]
        public ObjectLink Html { get; set; }
    }

    [DebuggerDisplay("{QueryType}")]
    public abstract class QueryResult
    {
        [JsonProperty(PropertyName = "asOf")]
        public DateTime AsOf { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public List<Field> Columns { get; set; }

        [JsonProperty(PropertyName = "queryType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public QueryType QueryType { get; set; }

        [JsonProperty(PropertyName = "sortColumns")]
        public List<SortColumn> SortColumns { get; set; }
    }

    [DebuggerDisplay("{Field.ReferenceName}")]
    public class SortColumn
    {
        [JsonProperty(PropertyName = "descending")]
        public bool Descending { get; set; }

        [JsonProperty(PropertyName = "field")]
        public Field Field { get; set; }
    }
}