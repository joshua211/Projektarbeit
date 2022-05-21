using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Userdata.Commands
{
    public class SubscribeToDocumentCommandHandler : CommandHandler<UserdataAggregate, UserdataId, IExecutionResult,
        SubscribeToDocumentCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(UserdataAggregate aggregate, SubscribeToDocumentCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.SubscribeToDocument(command.Document));
        }
    }
}