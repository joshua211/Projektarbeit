using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using HT.Application.Documents.Models;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using HT.Core.Aggregates.Document.Events;

namespace HT.Application.Documents
{
    public class DocumentReadModel : IMongoDbReadModel,
        IAmReadModelFor<DocumentAggregate, DocumentId, DocumentCreated>,
        IAmReadModelFor<DocumentAggregate, DocumentId, DocumentMarkedForDeletion>,
        IAmReadModelFor<DocumentAggregate, DocumentId, DocumentRevised>,
        IAmReadModelFor<DocumentAggregate, DocumentId, DocumentDeleted>,
        IAmReadModelFor<DocumentAggregate, DocumentId, DocumentRestored>,
        IAmReadModelFor<DocumentAggregate, DocumentId, VisibilityChanged>

    {
        public DocumentReadModel()
        {
            Revisions = new List<DocumentRevision>();
            Roles = new List<string>();
        }

        public string Category { get; set; }
        public UserContext UserContext { get; set; }
        public List<DocumentRevision> Revisions { get; set; }
        public List<string> Roles { get; set; }
        public bool MarkedForDeletion { get; set; }
        public DateTime DeletionTime { get; set; }
        public bool IsVisible { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<DocumentAggregate, DocumentId, DocumentCreated> domainEvent)
        {
            var ev = domainEvent.AggregateEvent;

            Id = domainEvent.AggregateIdentity.Value;
            UserContext = UserContext.FromUser(ev.InitialRevision.User);
            Revisions.Add(new DocumentRevision(UserContext, ev.InitialRevision.Date, ev.InitialRevision.Content,
                ev.InitialRevision.Title));
            Category = ev.Category.Name;
            Roles = ev.Roles.Select(r => r.Name).ToList();
            IsVisible = domainEvent.AggregateEvent.Visibility.IsVisible;
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<DocumentAggregate, DocumentId, DocumentDeleted> domainEvent)
        {
            context.MarkForDeletion();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<DocumentAggregate, DocumentId, DocumentMarkedForDeletion> domainEvent)
        {
            MarkedForDeletion = true;
            DeletionTime = domainEvent.AggregateEvent.DeletionMarker.DeletionTime;
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<DocumentAggregate, DocumentId, DocumentRestored> domainEvent)
        {
            MarkedForDeletion = false;
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<DocumentAggregate, DocumentId, DocumentRevised> domainEvent)
        {
            var ev = domainEvent.AggregateEvent;
            UserContext = UserContext.FromUser(ev.Revision.User);
            Revisions.Add(new DocumentRevision(UserContext, ev.Revision.Date, ev.Revision.Content, ev.Revision.Title));
            Category = ev.Category.Name;
            Roles = ev.Roles.Select(r => r.Name).ToList();
        }


        public void Apply(IReadModelContext context,
            IDomainEvent<DocumentAggregate, DocumentId, VisibilityChanged> domainEvent)
        {
            IsVisible = domainEvent.AggregateEvent.Visibility.IsVisible;
        }

        public string Id { get; private set; }
        public long? Version { get; set; }
    }
}