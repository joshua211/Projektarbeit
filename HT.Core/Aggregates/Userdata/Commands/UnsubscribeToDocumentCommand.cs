using EventFlow.Commands;
using HT.Core.Aggregates.Document;

namespace HT.Core.Aggregates.Userdata.Commands
{
    public class UnsubscribeToDocumentCommand : Command<UserdataAggregate, UserdataId>
    {
        public UnsubscribeToDocumentCommand(UserdataId aggregateId, DocumentId document) : base(aggregateId)
        {
            Document = document;
        }

        public DocumentId Document { get; private set; }
    }
}