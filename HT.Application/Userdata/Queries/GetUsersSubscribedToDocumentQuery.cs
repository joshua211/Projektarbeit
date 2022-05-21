using System.Collections.Generic;
using EventFlow.Queries;
using HT.Core.Aggregates.Document;

namespace HT.Application.Userdata.Queries
{
    public class GetUsersSubscribedToDocumentQuery : IQuery<IEnumerable<UserdataReadModel>>
    {
        public GetUsersSubscribedToDocumentQuery(DocumentId id)
        {
            Id = id;
        }

        public DocumentId Id { get; private set; }
    }
}