using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace HT.Core.Aggregates.Article.Commands
{
    public class CreateArticleCommandHandler : CommandHandler<ArticleAggregate, ArticleId, IExecutionResult,
        CreateArticleCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(ArticleAggregate aggregate,
            CreateArticleCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.CreateArticle(command.InitialRevision, command.Visibility));
        }
    }
}