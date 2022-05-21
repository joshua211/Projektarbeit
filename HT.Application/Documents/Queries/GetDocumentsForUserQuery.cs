using System.Collections.Generic;
using EventFlow.Queries;

namespace HT.Application.Documents.Queries
{
    public class GetDocumentsForUserQuery : IQuery<IEnumerable<DocumentReadModel>>
    {
        public GetDocumentsForUserQuery(IEnumerable<string> roles)
        {
            Roles = roles;
        }

        public IEnumerable<string> Roles { get; private set; }
    }
}