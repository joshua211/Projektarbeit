using System.Collections.Generic;
using EventFlow.Aggregates;
using HT.Core.Aggregates.Document;
using HT.Core.Aggregates.Userdata.Events;
using HT.Core.Exceptions;
using HT.Core.Shared.ValueObjects;

namespace HT.Core.Aggregates.Userdata
{
    public class UserdataState : IEventApplier<UserdataAggregate, UserdataId>
    {
        public List<DocumentId> SubscribedDocuments;
        public User User;

        public UserdataState()
        {
            SubscribedDocuments = new List<DocumentId>();
        }

        public bool Apply(UserdataAggregate aggregate, IAggregateEvent<UserdataAggregate, UserdataId> aggregateEvent) =>
            aggregateEvent switch
            {
                UserdataCreated created => ApplyUserdataCreated(created),
                SubscribedToDocument subscribed => ApplySubscribedToDocument(subscribed),
                UnsubscribedToDocument unsubscribed => ApplyUnsubscribedToDocument(unsubscribed),
                _ => throw new EventNotHandledException(aggregateEvent)
            };

        public bool ApplyUserdataCreated(UserdataCreated created)
        {
            User = created.User;
            
            return true;
        }

        public bool ApplySubscribedToDocument(SubscribedToDocument subscribed)
        {
            SubscribedDocuments.Add(subscribed.Document);
            
            return true;
        }

        public bool ApplyUnsubscribedToDocument(UnsubscribedToDocument unsubscribed)
        {
            SubscribedDocuments.Remove(unsubscribed.Document);
            
            return true;
        }
    }
}