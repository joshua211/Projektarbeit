using EventFlow.Aggregates;

namespace HT.Core.Aggregates.Article.Events
{
    public class ArticleDeleted : AggregateEvent<ArticleAggregate, ArticleId>
    {
    }
}