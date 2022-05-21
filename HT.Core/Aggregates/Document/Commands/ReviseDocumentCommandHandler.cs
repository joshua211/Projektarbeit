using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Document.Commands
{
    public class
        ReviseDocumentCommandHandler : CommandHandler<DocumentAggregate, DocumentId, IExecutionResult,
            ReviseDocumentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(DocumentAggregate aggregate,
            ReviseDocumentCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.ReviseDocument(command.Revision, command.Roles, command.Category));
        }
    }
}