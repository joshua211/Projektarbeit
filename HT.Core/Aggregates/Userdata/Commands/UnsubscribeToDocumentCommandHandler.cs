using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Userdata.Commands
{
    public class UnsubscribeToDocumentCommandHandler : CommandHandler<UserdataAggregate, UserdataId, IExecutionResult,
        UnsubscribeToDocumentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(UserdataAggregate aggregate, UnsubscribeToDocumentCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.UnsubscribeToDocument(command.Document));
        }
    }
}