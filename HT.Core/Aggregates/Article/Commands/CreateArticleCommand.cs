using EventFlow.Commands;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article.Commands
{
    public class CreateArticleCommand : Command<ArticleAggregate, ArticleId>
    {
        public CreateArticleCommand(ArticleId aggregateId, Revision initialRevision, Visibility visibility) : base(
            aggregateId)
        {
            InitialRevision = initialRevision;
            Visibility = visibility;
        }

        public Revision InitialRevision { get; private set; }
        public Visibility Visibility { get; private set; }
    }
}