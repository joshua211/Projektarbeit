using System.Collections.Generic;
using EventFlow.Specifications;
using HT.Core.Aggregates.Document;

namespace HT.Core.Aggregates.Userdata.Specs
{
    public class IsSubscribedSpec : ISpecification<UserdataState>
    {
        private DocumentId id;

        public IsSubscribedSpec(DocumentId id)
        {
            this.id = id;
        }

        public bool IsSatisfiedBy(UserdataState obj)
        {
            return obj.SubscribedDocuments.Contains(id);
        }

        public IEnumerable<string> WhyIsNotSatisfiedBy(UserdataState obj)
        {
            if (!IsSatisfiedBy(obj))
                yield return "User is not subscribed to " + id;
        }

        public static IsSubscribedSpec With(DocumentId id) => new IsSubscribedSpec(id);
    }
}