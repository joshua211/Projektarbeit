using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Document.Commands
{
    public class ChangeVisibilityCommandHandler : CommandHandler<DocumentAggregate, DocumentId, IExecutionResult,
        ChangeVisibilityCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(DocumentAggregate aggregate,
            ChangeVisibilityCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.SetVisibility(command.User, command.Visibility));
        }
    }
}