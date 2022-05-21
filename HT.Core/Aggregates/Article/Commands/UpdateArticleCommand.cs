using EventFlow.Commands;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article.Commands
{
    public class UpdateArticleCommand : Command<ArticleAggregate, ArticleId>
    {
        public UpdateArticleCommand(ArticleId aggregateId, Revision revision) : base(aggregateId)
        {
            Revision = revision;
        }

        public Revision Revision { get; private set; }
    }
}