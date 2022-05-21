using System.Collections.Generic;
using EventFlow.Specifications;

namespace HT.Core.Aggregates.Document.Specs
{
    public class CanBeMarkedSpec : ISpecification<DocumentState>
    {
        public static CanBeMarkedSpec Spec => new CanBeMarkedSpec();

        public bool IsSatisfiedBy(DocumentState obj)
        {
            return obj.DeletionMarker is null;
        }

        public IEnumerable<string> WhyIsNotSatisfiedBy(DocumentState obj)
        {
            if (IsSatisfiedBy(obj))
                yield break;
            yield return "Document ist already marked for deletion";
        }
    }
}