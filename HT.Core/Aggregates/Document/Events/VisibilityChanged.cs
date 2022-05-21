using EventFlow.Aggregates;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document.Events
{
    public class VisibilityChanged : AggregateEvent<DocumentAggregate, DocumentId>
    {
        public VisibilityChanged(Visibility visibility)
        {
            Visibility = visibility;
        }

        public Visibility Visibility { get; private set; }
    }
}