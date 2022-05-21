using System;
using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using HT.Application.Shared;
using HT.Core.Aggregates.Userdata;
using HT.Core.Aggregates.Userdata.Events;

namespace HT.Application.Userdata
{
    public class UserdataReadModel : IMongoDbReadModel, 
                                    IAmReadModelFor<UserdataAggregate, UserdataId, UserdataCreated>,
                                    IAmReadModelFor<UserdataAggregate, UserdataId, SubscribedToDocument>,
                                    IAmReadModelFor<UserdataAggregate, UserdataId, UnsubscribedToDocument>
    {
        public UserdataReadModel()
        {
            SubscribedDocuments = new List<string>();
        }

        public List<string> SubscribedDocuments { get; set; }
        public Guid UserId { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<UserdataAggregate, UserdataId, SubscribedToDocument> domainEvent)
        {
            SubscribedDocuments.Add(domainEvent.AggregateEvent.Document.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserdataAggregate, UserdataId, UnsubscribedToDocument> domainEvent)
        {
            SubscribedDocuments.Remove(domainEvent.AggregateEvent.Document.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserdataAggregate, UserdataId, UserdataCreated> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            UserId = UserContext.FromUser(domainEvent.AggregateEvent.User).Id;
        }

        public string Id { get; set; }
        public long? Version { get; set; }
    }
}