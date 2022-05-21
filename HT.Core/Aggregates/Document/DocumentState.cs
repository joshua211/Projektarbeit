using System.Collections.Generic;
using EventFlow.Aggregates;
using HT.Core.Aggregates.Document.Events;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Exceptions;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document
{
    public class DocumentState : IEventApplier<DocumentAggregate, DocumentId>
    {
        public Category Category;
        public DeletionMarker DeletionMarker;
        public User DocumentCreator;
        public List<Revision> Revisions;
        public List<Role> Roles;
        public Visibility Visibility;
        public bool WasDeleted;

        public DocumentState()
        {
            Revisions = new List<Revision>();
            Roles = new List<Role>();
        }

        public bool Apply(DocumentAggregate aggregate, IAggregateEvent<DocumentAggregate, DocumentId> aggregateEvent) =>
            aggregateEvent switch
            {
                DocumentCreated created => ApplyDocumentCreated(created),
                DocumentMarkedForDeletion removed => ApplyDocumentMarkedForDeletion(removed),
                DocumentRevised revised => ApplyDocumentRevised(revised),
                DocumentRestored restored => ApplyDocumentRestored(restored),
                DocumentDeleted deleted => ApplyDocumentDeleted(deleted),
                VisibilityChanged visibilityChanged => ApplyVisibilityChanged(visibilityChanged),
                _ => throw new EventNotHandledException(aggregateEvent)
            };

        public bool ApplyVisibilityChanged(VisibilityChanged visibilityChanged)
        {
            Visibility = visibilityChanged.Visibility;
            return true;
        }

        public bool ApplyDocumentRevised(DocumentRevised revised)
        {
            Revisions.Add(revised.Revision);
            Category = revised.Category;
            Roles = revised.Roles;

            return true;
        }

        public bool ApplyDocumentCreated(DocumentCreated created)
        {
            Revisions.Add(created.InitialRevision);
            Roles = created.Roles;
            Category = created.Category;
            Visibility = created.Visibility;

            return true;
        }

        public bool ApplyDocumentMarkedForDeletion(DocumentMarkedForDeletion markedForDeletion)
        {
            DeletionMarker = markedForDeletion.DeletionMarker;

            return true;
        }

        public bool ApplyDocumentRestored(DocumentRestored documentRestored)
        {
            DeletionMarker = null;

            return true;
        }

        public bool ApplyDocumentDeleted(DocumentDeleted deleted)
        {
            WasDeleted = true;

            return true;
        }
    }
}