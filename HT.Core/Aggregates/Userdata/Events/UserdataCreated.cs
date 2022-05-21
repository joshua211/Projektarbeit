using EventFlow.Aggregates;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Userdata.Events
{
    public class UserdataCreated : AggregateEvent<UserdataAggregate, UserdataId>
    {
        public UserdataCreated(User user)
        {
            User = user;
        }

        public User User { get; private set; }
    }
}