using EventFlow.Aggregates;
using HT.Core.Aggregates.Document;

namespace HT.Core.Aggregates.Userdata.Events
{
    public class UnsubscribedToDocument : AggregateEvent<UserdataAggregate, UserdataId>
    {
        public UnsubscribedToDocument(DocumentId document)
        {
            Document = document;
        }

        public DocumentId Document { get; private set; }
    }
}