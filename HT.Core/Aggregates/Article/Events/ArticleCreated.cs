using EventFlow.Aggregates;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article.Events
{
    public class ArticleCreated : AggregateEvent<ArticleAggregate, ArticleId>
    {
        public ArticleCreated(Revision initialRevision, Visibility visibility)
        {
            InitialRevision = initialRevision;
            Visibility = visibility;
        }

        public Revision InitialRevision { get; private set; }
        public Visibility Visibility { get; private set; }
    }
}