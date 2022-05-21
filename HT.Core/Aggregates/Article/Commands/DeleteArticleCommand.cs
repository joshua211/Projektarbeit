using EventFlow.Commands;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article.Commands
{
    public class DeleteArticleCommand : Command<ArticleAggregate, ArticleId>
    {
        public DeleteArticleCommand(ArticleId aggregateId, User user) : base(aggregateId)
        {
            User = user;
        }

        public User User { get; private set; }
    }
}