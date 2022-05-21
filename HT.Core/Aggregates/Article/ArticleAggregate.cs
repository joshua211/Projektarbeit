using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Extensions;
using HT.Core.Aggregates.Article.Events;
using HT.Core.Aggregates.Article.Specs;
using HT.Core.Shared;
using HT.Core.Shared.Specs;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Article
{
    public class ArticleAggregate : AggregateRoot<ArticleAggregate, ArticleId>
    {
        private readonly ArticleState state;

        public ArticleAggregate(ArticleId id) : base(id)
        {
            state = new ArticleState();
            Register(state);
        }

        public IExecutionResult CreateArticle(Revision revision, Visibility visibility)
        {
            IsUserInRoleSpec.With(Roles.ArticleWriter).ThrowDomainErrorIfNotSatisfied(revision.User);

            Emit(new ArticleCreated(revision, visibility));

            return new SuccessExecutionResult();
        }

        public IExecutionResult DeleteArticle(User user)
        {
            IsUserInRoleSpec.With(Roles.ArticleWriter).ThrowDomainErrorIfNotSatisfied(user);
            IsNotDeleted.Spec.ThrowDomainErrorIfNotSatisfied(state);

            Emit(new ArticleDeleted());

            return new SuccessExecutionResult();
        }

        public IExecutionResult UpdateArticle(Revision revision)
        {
            IsUserInRoleSpec.With(Roles.ArticleWriter).ThrowDomainErrorIfNotSatisfied(revision.User);
            IsNotDeleted.Spec.ThrowDomainErrorIfNotSatisfied(state);

            Emit(new ArticleUpdated(revision));

            return new SuccessExecutionResult();
        }

        public IExecutionResult ChangeVisibility(Visibility visibility, User user)
        {
            IsUserInRoleSpec.With(Roles.ArticleWriter).ThrowDomainErrorIfNotSatisfied(user);
            IsNotDeleted.Spec.ThrowDomainErrorIfNotSatisfied(state);

            Emit(new VisibilityChanged(visibility));

            return new SuccessExecutionResult();
        }
    }
}