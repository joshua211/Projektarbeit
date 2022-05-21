using System.Collections.Generic;
using EventFlow.Specifications;

namespace HT.Core.Aggregates.Article.Specs
{
    public class IsNotDeleted : ISpecification<ArticleState>
    {
        public static IsNotDeleted Spec => new IsNotDeleted();

        public bool IsSatisfiedBy(ArticleState obj)
        {
            return obj.WasDeleted == false && obj.LatestRevision != null;
        }

        public IEnumerable<string> WhyIsNotSatisfiedBy(ArticleState obj)
        {
            if (!IsSatisfiedBy(obj))
                yield return "Article has already been deleted";
        }
    }
}