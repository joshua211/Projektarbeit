using EventFlow.Aggregates;

namespace HT.Core.Aggregates.Document.Events
{
    public class DocumentDeleted : AggregateEvent<DocumentAggregate, DocumentId>
    {
    }
}