using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Article.Commands
{
    public class UpdateArticleCommandHandler : CommandHandler<ArticleAggregate, ArticleId, IExecutionResult,
        UpdateArticleCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ArticleAggregate aggregate,
            UpdateArticleCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.UpdateArticle(command.Revision));
        }
    }
}