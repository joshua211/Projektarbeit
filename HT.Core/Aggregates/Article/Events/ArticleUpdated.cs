using EventFlow.Aggregates;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article.Events
{
    public class ArticleUpdated : AggregateEvent<ArticleAggregate, ArticleId>
    {
        public ArticleUpdated(Revision revision)
        {
            Revision = revision;
        }

        public Revision Revision { get; private set; }
    }
}