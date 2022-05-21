using EventFlow.Commands;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article.Commands
{
    public class ChangeVisibilityCommand : Command<ArticleAggregate, ArticleId>
    {
        public ChangeVisibilityCommand(ArticleId aggregateId, Visibility visibility, User user) : base(aggregateId)
        {
            Visibility = visibility;
            User = user;
        }

        public Visibility Visibility { get; private set; }
        public User User { get; private set; }
    }
}