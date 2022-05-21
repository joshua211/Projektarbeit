using System.Collections.Generic;
using EventFlow.Aggregates;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document.Events
{
    public class DocumentRevised : AggregateEvent<DocumentAggregate, DocumentId>
    {
        public DocumentRevised(Revision revision, List<Role> roles, Category category)
        {
            Revision = revision;
            Roles = roles;
            Category = category;
        }

        public Revision Revision { get; private set; }
        public List<Role> Roles { get; private set; }
        public Category Category { get; private set; }
    }
}