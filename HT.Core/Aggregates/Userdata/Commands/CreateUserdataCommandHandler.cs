using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Userdata.Commands
{
    public class CreateUserdataCommandHandler : CommandHandler<UserdataAggregate, UserdataId, IExecutionResult,
        CreateUserdataCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(UserdataAggregate aggregate, CreateUserdataCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.CreateUserdata(command.User));
        }
    }
}