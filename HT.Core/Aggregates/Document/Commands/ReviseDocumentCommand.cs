using System.Collections.Generic;
using EventFlow.Commands;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document.Commands
{
    public class ReviseDocumentCommand : Command<DocumentAggregate, DocumentId>
    {
        public ReviseDocumentCommand(DocumentId aggregateId, Revision revision, List<Role> roles,
            Category category) : base(aggregateId)
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