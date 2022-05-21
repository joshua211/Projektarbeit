using EventFlow.Commands;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document.Commands
{
    public class MarkDocumentForDeletionCommand : Command<DocumentAggregate, DocumentId>
    {
        public MarkDocumentForDeletionCommand(DocumentId aggregateId, DeletionMarker marker, User user) : base(
            aggregateId)
        {
            Marker = marker;
            User = user;
        }

        public DeletionMarker Marker { get; private set; }
        public User User { get; private set; }
    }
}