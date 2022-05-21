using EventFlow.Aggregates;
using HT.Core.Aggregates.Document.ValueObjects;

namespace HT.Core.Aggregates.Document.Events
{
    public class DocumentMarkedForDeletion : AggregateEvent<DocumentAggregate, DocumentId>
    {
        public DocumentMarkedForDeletion(DeletionMarker deletionMarker)
        {
            DeletionMarker = deletionMarker;
        }

        public DeletionMarker DeletionMarker { get; private set; }
    }
}