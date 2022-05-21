using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Extensions;
using HT.Core.Aggregates.Document.Events;
using HT.Core.Aggregates.Document.Specs;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Shared;
using HT.Core.Shared.Specs;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document
{
    public class DocumentAggregate : AggregateRoot<DocumentAggregate, DocumentId>
    {
        private readonly DocumentState state;

        public DocumentAggregate(DocumentId id) : base(id)
        {
            state = new DocumentState();
            Register(state);
        }

        public IExecutionResult CreateDocument(Revision revision, Visibility visibility, User creator, List<Role> roles,
            Category category)
        {
            IsUserInRoleSpec.With(Roles.DocumentWriter).ThrowDomainErrorIfNotSatisfied(creator);

            Emit(new DocumentCreated(revision, visibility, creator, roles, category));

            return new SuccessExecutionResult();
        }

        public IExecutionResult DeleteDocument()
        {
            CanBeDeletedSpec.Spec.ThrowDomainErrorIfNotSatisfied(state);

            Emit(new DocumentDeleted());

            return new SuccessExecutionResult();
        }

        public IExecutionResult ReviseDocument(Revision revision, List<Role> roles,
            Category category)
        {
            IsUserInRoleSpec.With(Roles.DocumentWriter).ThrowDomainErrorIfNotSatisfied(revision.User);
            IsNotDeleted.Spec.ThrowDomainErrorIfNotSatisfied(state);

            roles ??= state.Roles;
            category ??= state.Category;
            Emit(new DocumentRevised(revision, roles, category));

            return new SuccessExecutionResult();
        }

        public IExecutionResult MarkDocumentForDeletion(User user, DeletionMarker marker)
        {
            CanBeMarkedSpec.Spec.ThrowDomainErrorIfNotSatisfied(state);
            IsUserInRoleSpec.With(Roles.DocumentWriter).ThrowDomainErrorIfNotSatisfied(user);
            Emit(new DocumentMarkedForDeletion(marker));

            return new SuccessExecutionResult();
        }

        public IExecutionResult RestoreDocument(User user)
        {
            IsUserInRoleSpec.With(Roles.DocumentWriter).ThrowDomainErrorIfNotSatisfied(user);
            IsNotDeleted.Spec.Not().ThrowDomainErrorIfNotSatisfied(state);

            Emit(new DocumentRestored());

            return ExecutionResult.Success();
        }

        public IExecutionResult SetVisibility(User user, Visibility visibility)
        {
            IsUserInRoleSpec.With(Roles.DocumentWriter).ThrowDomainErrorIfNotSatisfied(user);
            IsNotDeleted.Spec.ThrowDomainErrorIfNotSatisfied(state);

            Emit(new VisibilityChanged(visibility));

            return new SuccessExecutionResult();
        }
    }
}