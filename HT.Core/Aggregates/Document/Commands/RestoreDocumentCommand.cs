using EventFlow.Commands;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document.Commands
{
    public class RestoreDocumentCommand : Command<DocumentAggregate, DocumentId>
    {
        public RestoreDocumentCommand(DocumentId aggregateId, User user) : base(aggregateId)
        {
            User = user;
        }

        public User User { get; private set; }
    }
}