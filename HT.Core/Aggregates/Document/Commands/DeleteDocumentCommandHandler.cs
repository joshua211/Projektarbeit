using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Document.Commands
{
    public class
        RemoveDocumentCommandHandler : CommandHandler<DocumentAggregate, DocumentId, IExecutionResult,
            DeleteDocumentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(DocumentAggregate aggregate,
            DeleteDocumentCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.DeleteDocument());
        }
    }
}