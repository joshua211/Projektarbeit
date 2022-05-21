using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Document.Commands
{
    public class MarkDocumentForDeletionCommandHandler : CommandHandler<DocumentAggregate, DocumentId, IExecutionResult,
        MarkDocumentForDeletionCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(DocumentAggregate aggregate,
            MarkDocumentForDeletionCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.MarkDocumentForDeletion(command.User, command.Marker));
        }
    }
}