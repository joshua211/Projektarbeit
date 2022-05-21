using EventFlow.Aggregates;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article.Events
{
    public class VisibilityChanged : AggregateEvent<ArticleAggregate, ArticleId>
    {
        public VisibilityChanged(Visibility visibility)
        {
            Visibility = visibility;
        }

        public Visibility Visibility { get; private set; }
    }
}