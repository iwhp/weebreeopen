namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public enum OperationType
    {
        add,
        remove,
        replace,
        test
    }

    public enum TopologyType
    {
        dependency,
        network,
        tree
    }

    [DebuggerDisplay("{OldValue}->{NewValue}")]
    public class FieldChange
    {
        [JsonProperty(PropertyName = "newValue")]
        public object NewValue { get; set; }

        [JsonProperty(PropertyName = "oldValue")]
        public object OldValue { get; set; }
    }

    [DebuggerDisplay("{Value}")]
    public class HistoryComment : BaseObject
    {
        [JsonProperty(PropertyName = "rev")]
        public int Rev { get; set; }

        [JsonProperty(PropertyName = "revisedBy")]
        public User RevisedBy { get; set; }

        [JsonProperty(PropertyName = "revisedDate")]
        public DateTime RevisedDate { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    [DebuggerDisplay("{Id:Name}")]
    public class RelationAttributes
    {
        [JsonProperty(PropertyName = "authorizedDate")]
        public DateTime AuthorizedDate { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "isLocked")]
        public bool? IsLocked { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "resourceCreatedDate")]
        public DateTime ResourceCreatedDate { get; set; }

        [JsonProperty(PropertyName = "resourceModifiedDate")]
        public DateTime ResourceModifiedDate { get; set; }

        [JsonProperty(PropertyName = "resourceSize")]
        public int ResourceSize { get; set; }

        [JsonProperty(PropertyName = "revisedDate")]
        public DateTime RevisedDate { get; set; }

        public override bool Equals(object obj)
        {
            var attributes = obj as RelationAttributes;
            if (attributes == null)
            {
                return false;
            }

            return this.Id == attributes.Id && this.AuthorizedDate == attributes.AuthorizedDate;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.AuthorizedDate.GetHashCode();
        }
    }

    public class RelationChanges
    {
        [JsonProperty(PropertyName = "added")]
        public List<WorkItemRelation> AddedRelations { get; set; }

        [JsonProperty(PropertyName = "removed")]
        public List<WorkItemRelation> RemovedRelations { get; set; }
    }

    public class RelationTypeAttributes
    {
        [JsonProperty(PropertyName = "acyclic")]
        public bool Acyclic { get; set; }

        [JsonProperty(PropertyName = "directional")]
        public bool Directional { get; set; }

        [JsonProperty(PropertyName = "editable")]
        public bool Editable { get; set; }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "singleTarget")]
        public bool SingleTarget { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "topology")]
        public TopologyType Topology { get; set; }

        [JsonProperty(PropertyName = "usage")]
        public string Usage { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class User : ObjectWithId<string>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class WorkItem : WorkItemCore
    {
        public WorkItem()
        {
            this.Relations = new ObservableCollection<WorkItemRelation>();
            this.Fields = new ObservableDictionary<string, object>();

            this.Relations.CollectionChanged += this.OnRelations_CollectionChanged;
            this.Fields.CollectionChanged += this.OnFields_CollectionChanged;
        }

        [JsonProperty(PropertyName = "fields")]
        public ObservableDictionary<string, object> Fields;
        [JsonProperty(PropertyName = "_links")]
        public WorkItemLink References;
        internal List<FieldUpdate> FieldUpdates = new List<FieldUpdate>();
        internal List<RelationUpdate> RelationUpdates = new List<RelationUpdate>();

        [JsonProperty(PropertyName = "relations")]
        public ObservableCollection<WorkItemRelation> Relations { get; set; }

        private void OnFields_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                //TODO: NotifyCollectionChangedAction.Replace
                if (e.NewItems != null)
                {
                    foreach (KeyValuePair<string, object> newfield in e.NewItems)
                    {
                        var existingUpdate = this.FieldUpdates.FirstOrDefault(fu => fu.Name == newfield.Key);
                        if (existingUpdate != null)
                        {
                            this.FieldUpdates.Remove(existingUpdate);
                        }

                        this.FieldUpdates.Add(new FieldUpdate(newfield.Key, newfield.Value, (OperationType)e.Action /*TODO*/));
                    }
                }

                if (e.Action != NotifyCollectionChangedAction.Replace && e.OldItems != null)
                {
                    foreach (KeyValuePair<string, object> removedField in e.OldItems)
                    {
                        var existingUpdate = this.FieldUpdates.FirstOrDefault(fu => fu.Name == removedField.Key);
                        if (existingUpdate != null)
                        {
                            this.FieldUpdates.Remove(existingUpdate);
                        }

                        this.FieldUpdates.Add(new FieldUpdate(removedField.Key, (OperationType)e.Action /*TODO*/));
                    }
                }
            }
        }

        private void OnRelations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.NewItems != null)
                {
                    foreach (WorkItemRelation newRelation in e.NewItems)
                    {
                        var existingUpdate = this.RelationUpdates.FirstOrDefault(ru => ru.Value == newRelation);
                        if (existingUpdate != null)
                        {
                            if (existingUpdate.Operation == OperationType.remove)
                            {
                                this.RelationUpdates.Remove(existingUpdate);
                            }
                        }
                        else
                        {
                            this.RelationUpdates.Add(new RelationUpdate(newRelation, OperationType.add));
                        }
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (WorkItemRelation oldRelation in e.OldItems)
                    {
                        var existingUpdate = this.RelationUpdates.FirstOrDefault(ru => ru.Value == oldRelation);
                        if (existingUpdate != null)
                        {
                            if (existingUpdate.Operation == OperationType.add)
                            {
                                this.RelationUpdates.Remove(existingUpdate);
                            }
                        }
                        else
                        {
                            this.RelationUpdates.Add(new RelationUpdate(oldRelation, OperationType.remove));
                        }
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Remove all links
                this.RelationUpdates.Clear();
                this.RelationUpdates.Add(new RelationUpdate() { Operation = OperationType.remove });
            }
        }
    }

    [DebuggerDisplay("{Id:Rev}")]
    public abstract class WorkItemCore : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "rev")]
        public int Rev { get; set; }
    }

    public class WorkItemLink : ObjectLink
    {
        [JsonProperty(PropertyName = "fields")]
        public ObjectLink Fields { get; set; }

        [JsonProperty(PropertyName = "workItemHistory")]
        public ObjectLink History { get; set; }

        [JsonProperty(PropertyName = "html")]
        public ObjectLink Html { get; set; }

        [JsonProperty(PropertyName = "workItemRevisions")]
        public ObjectLink Revisions { get; set; }

        [JsonProperty(PropertyName = "workItemType")]
        public ObjectLink Type { get; set; }

        [JsonProperty(PropertyName = "workItemUpdates")]
        public ObjectLink Updates { get; set; }
    }

    [DebuggerDisplay("{Rel}")]
    public class WorkItemRelation : BaseObject
    {
        [JsonProperty(PropertyName = "attributes")]
        public RelationAttributes Attributes { get; set; }

        [JsonProperty(PropertyName = "rel")]
        public string Rel { get; set; }

        [JsonProperty(PropertyName = "source")]
        public WorkItem Source { get; set; }

        [JsonProperty(PropertyName = "target")]
        public WorkItem Target { get; set; }

        [JsonIgnore]
        internal int Index { get; set; }

        public override bool Equals(object obj)
        {
            var relation = obj as WorkItemRelation;
            if (relation == null)
            {
                return false;
            }

            return relation.Rel == this.Rel && relation.Attributes == this.Attributes && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (this.Rel != null ? this.Rel.GetHashCode() : 1) ^ (this.Attributes != null ? this.Attributes.GetHashCode() : 1);
        }
    }

    [DebuggerDisplay("{ReferenceName}")]
    public class WorkItemRelationType : BaseObject
    {
        [JsonProperty(PropertyName = "attributes")]
        public RelationTypeAttributes Attributes { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "referenceName")]
        public string ReferenceName { get; set; }
    }

    public class WorkItemUpdate : WorkItemCore
    {
        [JsonProperty(PropertyName = "fields")]
        public Dictionary<string, FieldChange> FieldChanges;

        [JsonProperty(PropertyName = "relations")]
        public RelationChanges Changes { get; set; }

        [JsonProperty(PropertyName = "revisedBy")]
        public User RevisedBy { get; set; }

        [JsonProperty(PropertyName = "revisedDate")]
        public DateTime RevisedDate { get; set; }
    }

    internal class FieldUpdate : Update
    {
        public FieldUpdate(string referenceName, OperationType operation)
        {
            this.Operation = operation;
            this.Name = referenceName;
            this.Path = string.Format("/fields/{0}", referenceName);
        }

        public FieldUpdate(string referenceName, object value, OperationType operation = OperationType.add)
            : this(referenceName, operation)
        {
            this.Value = value;
        }

        [JsonIgnore]
        public string Name { get; set; }
    }

    internal class RelationUpdate : Update
    {
        public RelationUpdate()
        {
            this.Path = "/relations/-";
        }

        public RelationUpdate(WorkItemRelation relation, OperationType operation)
        {
            this.Operation = operation;
            this.Path = operation == OperationType.add ? "/relations/-" : string.Format("/relations/{0}", relation.Index);

            if (operation != OperationType.remove)
            {
                var attributeDictionary = new Dictionary<string, object>();
                if (relation.Attributes.Comment != null)
                {
                    attributeDictionary.Add("comment", relation.Attributes.Comment);
                }
                if (relation.Attributes.IsLocked != null)
                {
                    attributeDictionary.Add("isLocked", relation.Attributes.IsLocked.Value);
                }

                this.Value = new { rel = relation.Rel, url = relation.Url, attributes = attributeDictionary };
            }
        }
    }

    [DebuggerDisplay("{Path}")]
    internal abstract class Update
    {
        [JsonProperty(PropertyName = "op")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationType Operation { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }
    }
}