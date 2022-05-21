using EventFlow.Commands;

namespace HT.Core.Aggregates.Document.Commands
{
    public class DeleteDocumentCommand : Command<DocumentAggregate, DocumentId>
    {
        public DeleteDocumentCommand(DocumentId aggregateId) : base(aggregateId)
        {
        }
    }
}