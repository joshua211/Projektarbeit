using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Document.Commands
{
    public class RestoreDocumentCommandHandler : CommandHandler<DocumentAggregate, DocumentId, IExecutionResult,
        RestoreDocumentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(DocumentAggregate aggregate,
            RestoreDocumentCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.RestoreDocument(command.User));
        }
    }
}