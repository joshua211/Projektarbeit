using System;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using HT.Application.Article.Models;
using HT.Application.Shared;
using HT.Core.Aggregates.Article;
using HT.Core.Aggregates.Article.Events;

namespace HT.Application.Article
{
    public class ArticleReadModel : IMongoDbReadModel,
        IAmReadModelFor<ArticleAggregate, ArticleId, ArticleCreated>,
        IAmReadModelFor<ArticleAggregate, ArticleId, ArticleDeleted>,
        IAmReadModelFor<ArticleAggregate, ArticleId, ArticleUpdated>,
        IAmReadModelFor<ArticleAggregate, ArticleId, VisibilityChanged>


    {
        public ArticleRevision CurrentRevision { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreationTime { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<ArticleAggregate, ArticleId, ArticleCreated> domainEvent)
        {
            var ev = domainEvent.AggregateEvent;

            Id = domainEvent.AggregateIdentity.Value;
            CurrentRevision = new ArticleRevision(UserContext.FromUser(ev.InitialRevision.User),
                ev.InitialRevision.Date, ev.InitialRevision.Content, ev.InitialRevision.Title);
            IsVisible = ev.Visibility.IsVisible;
            CreationTime = CurrentRevision.Date;
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<ArticleAggregate, ArticleId, ArticleDeleted> domainEvent)
        {
            context.MarkForDeletion();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<ArticleAggregate, ArticleId, ArticleUpdated> domainEvent)
        {
            var ev = domainEvent.AggregateEvent;

            CurrentRevision = new ArticleRevision(UserContext.FromUser(ev.Revision.User),
                ev.Revision.Date, ev.Revision.Content, ev.Revision.Title);
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<ArticleAggregate, ArticleId, VisibilityChanged> domainEvent)
        {
            var ev = domainEvent.AggregateEvent;

            IsVisible = ev.Visibility.IsVisible;
        }

        public string Id { get; private set; }
        public long? Version { get; set; }
    }
}