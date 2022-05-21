using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;

namespace HT.Core.Aggregates.Document.Specs
{
    public class IsNotDeleted : ISpecification<DocumentState>
    {
        public static IsNotDeleted Spec => new IsNotDeleted();

        public bool IsSatisfiedBy(DocumentState obj)
        {
            return obj.Revisions.Any() && obj.DeletionMarker == null && !obj.WasDeleted;
        }

        public IEnumerable<string> WhyIsNotSatisfiedBy(DocumentState obj)
        {
            if (!obj.Revisions.Any())
                yield return "No Revisions exist for this document";
            if (obj.WasDeleted)
                yield return "Document has already been deleted";
            if (obj.DeletionMarker != null)
                yield return "Document has been marked for deletion";
        }
    }
}