using System.Collections.Generic;
using EventFlow.Aggregates;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document.Events
{
    public class DocumentCreated : AggregateEvent<DocumentAggregate, DocumentId>
    {
        public DocumentCreated(Revision initialRevision, Visibility visibility, User creator, List<Role> roles,
            Category category)
        {
            InitialRevision = initialRevision;
            Creator = creator;
            Roles = roles;
            Category = category;
            Visibility = visibility;
        }

        public Revision InitialRevision { get; private set; }
        public User Creator { get; private set; }
        public List<Role> Roles { get; private set; }
        public Category Category { get; private set; }
        public Visibility Visibility { get; private set; }
    }
}