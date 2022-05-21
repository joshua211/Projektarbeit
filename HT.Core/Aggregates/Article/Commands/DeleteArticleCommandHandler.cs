using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Article.Commands
{
    public class DeleteArticleCommandHandler : CommandHandler<ArticleAggregate, ArticleId, IExecutionResult,
        DeleteArticleCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ArticleAggregate aggregate,
            DeleteArticleCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.DeleteArticle(command.User));
        }
    }
}