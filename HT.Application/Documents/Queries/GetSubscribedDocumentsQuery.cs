using System.Collections.Generic;
using EventFlow.Queries;

namespace HT.Application.Documents.Queries
{
    public class GetSubscribedDocumentsQuery : IQuery<IEnumerable<DocumentReadModel>>
    {
        public GetSubscribedDocumentsQuery(IEnumerable<string> subscribedIds, IEnumerable<string> userRoles)
        {
            SubscribedIds = subscribedIds;
            UserRoles = userRoles;
        }

        public IEnumerable<string> SubscribedIds { get; private set; }
        public IEnumerable<string> UserRoles { get; private set; }
    }
}