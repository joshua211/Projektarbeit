using EventFlow.Commands;
using HT.Core.Aggregates.Document;

namespace HT.Core.Aggregates.Userdata.Commands
{
    public class SubscribeToDocumentCommand : Command<UserdataAggregate, UserdataId>
    {
        public SubscribeToDocumentCommand(UserdataId aggregateId, DocumentId document) : base(aggregateId)
        {
            Document = document;
        }

        public DocumentId Document { get; private set; }
    }
}