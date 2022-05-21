using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Article.Commands
{
    public class ChangeVisibilityCommandHandler : CommandHandler<ArticleAggregate, ArticleId, IExecutionResult,
        ChangeVisibilityCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ArticleAggregate aggregate,
            ChangeVisibilityCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.ChangeVisibility(command.Visibility, command.User));
        }
    }
}