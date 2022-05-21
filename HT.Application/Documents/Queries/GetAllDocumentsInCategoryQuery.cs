using System.Collections.Generic;
using EventFlow.Queries;

namespace HT.Application.Documents.Queries
{
    public class GetAllDocumentsInCategoryQuery : IQuery<IEnumerable<DocumentReadModel>>
    {
        public GetAllDocumentsInCategoryQuery(string category, IEnumerable<string> roles)
        {
            Category = category;
            Roles = roles;
        }

        public string Category { get; private set; }
        public IEnumerable<string> Roles { get; private set; }
    }
}