using System;
using System.Collections.Generic;
using EventFlow.Specifications;

namespace HT.Core.Aggregates.Document.Specs
{
    public class CanBeDeletedSpec : ISpecification<DocumentState>
    {
        public static CanBeDeletedSpec Spec => new CanBeDeletedSpec();

        public bool IsSatisfiedBy(DocumentState obj)
        {
            return obj.DeletionMarker != null && DateTime.Now >= obj.DeletionMarker.DeletionTime;
        }

        public IEnumerable<string> WhyIsNotSatisfiedBy(DocumentState obj)
        {
            if (IsSatisfiedBy(obj))
                yield break;

            if (obj.DeletionMarker is null)
                yield return "Document has not been marked for deletion";

            if (DateTime.Now <= obj.DeletionMarker?.DeletionTime)
                yield return "Document has not been marked for seven days";
        }
    }
}