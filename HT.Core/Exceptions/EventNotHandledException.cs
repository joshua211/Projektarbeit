using System;
using EventFlow.Aggregates;

namespace HT.Core.Exceptions
{
    public class EventNotHandledException : Exception
    {
        public EventNotHandledException(IAggregateEvent @event) : base("Event was not handled by the aggregate state")
        {
            Event = @event;
        }

        public IAggregateEvent Event { get; private set; }
    }
}