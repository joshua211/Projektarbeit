using System.Collections.Generic;
using EventFlow.Commands;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document.Commands
{
    public class CreateDocumentCommand : Command<DocumentAggregate, DocumentId>
    {
        public CreateDocumentCommand(DocumentId aggregateId, Revision initialRevision, Visibility visibility,
            User creator, List<Role> roles,
            Category category) : base(aggregateId)
        {
            InitialRevision = initialRevision;
            Visibility = visibility;
            Creator = creator;
            Roles = roles;
            Category = category;
        }

        public Revision InitialRevision { get; private set; }
        public User Creator { get; private set; }
        public List<Role> Roles { get; private set; }
        public Category Category { get; private set; }
        public Visibility Visibility { get; private set; }
    }
}