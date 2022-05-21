using EventFlow.Aggregates;

namespace HT.Core.Aggregates.Document.Events
{
    public class DocumentRestored : AggregateEvent<DocumentAggregate, DocumentId>
    {
    }
}