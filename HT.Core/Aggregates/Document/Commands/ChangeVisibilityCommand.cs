using EventFlow.Commands;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Document.Commands
{
    public class ChangeVisibilityCommand : Command<DocumentAggregate, DocumentId>
    {
        public ChangeVisibilityCommand(DocumentId aggregateId, User user, Visibility visibility) : base(aggregateId)
        {
            User = user;
            Visibility = visibility;
        }

        public User User { get; private set; }
        public Visibility Visibility { get; private set; }
    }
}