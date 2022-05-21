using EventFlow.Commands;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Userdata.Commands
{
    public class CreateUserdataCommand : Command<UserdataAggregate, UserdataId>
    {
        public CreateUserdataCommand(UserdataId aggregateId, User user) : base(aggregateId)
        {
            User = user;
        }

        public User User { get; private set; }
    }
}