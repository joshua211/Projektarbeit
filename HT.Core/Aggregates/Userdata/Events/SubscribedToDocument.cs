using EventFlow.Aggregates;
using HT.Core.Aggregates.Document;

namespace HT.Core.Aggregates.Userdata.Events
{
    public class SubscribedToDocument : AggregateEvent<UserdataAggregate, UserdataId>
    {
        public SubscribedToDocument(DocumentId document)
        {
            Document = document;
        }

        public DocumentId Document { get; private set; }
    }
}