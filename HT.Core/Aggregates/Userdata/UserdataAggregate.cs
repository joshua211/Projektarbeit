using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Extensions;
using HT.Core.Aggregates.Document;
using HT.Core.Aggregates.Userdata.Events;
using HT.Core.Aggregates.Userdata.Specs;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Userdata
{
    public class UserdataAggregate : AggregateRoot<UserdataAggregate, UserdataId>
    {
        private readonly UserdataState state;

        public UserdataAggregate(UserdataId id) : base(id)
        {
            state = new UserdataState();
            Register(state);
        }

        public IExecutionResult CreateUserdata(User user)
        {
            Emit(new UserdataCreated(user));
            
            return ExecutionResult.Success();
        }

        public IExecutionResult SubscribeToDocument(DocumentId id)
        {
            IsSubscribedSpec.With(id).Not().ThrowDomainErrorIfNotSatisfied(state);
            Emit(new SubscribedToDocument(id));

            return ExecutionResult.Success();
        }

        public IExecutionResult UnsubscribeToDocument(DocumentId id)
        {
            IsSubscribedSpec.With(id).ThrowDomainErrorIfNotSatisfied(state);
            Emit(new UnsubscribedToDocument(id));

            return ExecutionResult.Success();
        }
    }
}