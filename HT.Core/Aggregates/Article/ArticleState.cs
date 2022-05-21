using EventFlow.Aggregates;
using HT.Core.Aggregates.Article.Events;
using HT.Core.Exceptions;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article
{
    public class ArticleState : IEventApplier<ArticleAggregate, ArticleId>
    {
        public Revision LatestRevision { get; private set; }
        public Visibility Visibility { get; private set; }
        public bool WasDeleted { get; private set; }

        public bool Apply(ArticleAggregate aggregate, IAggregateEvent<ArticleAggregate, ArticleId> aggregateEvent) =>
            aggregateEvent switch
            {
                ArticleCreated created => ApplyArticleCreated(created),
                ArticleDeleted deleted => ApplyArticleDeleted(deleted),
                ArticleUpdated updated => ApplyArticleUpdated(updated),
                VisibilityChanged visibilityChanged => ApplyVisibilityChanged(visibilityChanged),
                _ => throw new EventNotHandledException(aggregateEvent)
            };

        private bool ApplyVisibilityChanged(VisibilityChanged visibilityChanged)
        {
            Visibility = visibilityChanged.Visibility;

            return true;
        }

        private bool ApplyArticleUpdated(ArticleUpdated updated)
        {
            LatestRevision = updated.Revision;

            return true;
        }

        private bool ApplyArticleDeleted(ArticleDeleted deleted)
        {
            return WasDeleted = true;
        }

        private bool ApplyArticleCreated(ArticleCreated created)
        {
            LatestRevision = created.InitialRevision;
            Visibility = created.Visibility;

            return true;
        }
    }
}