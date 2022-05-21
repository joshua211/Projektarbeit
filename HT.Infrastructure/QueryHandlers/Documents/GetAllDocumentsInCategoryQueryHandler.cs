using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using HT.Application.Documents;
using HT.Application.Documents.Queries;
using MongoDB.Driver;

namespace HT.Infrastructure.QueryHandlers.Documents
{
    public class GetAllDocumentsInCategoryQueryHandler : DocumentQueryHandler,
        IQueryHandler<GetAllDocumentsInCategoryQuery, IEnumerable<DocumentReadModel>>
    {
        public GetAllDocumentsInCategoryQueryHandler(IMongoDbReadModelStore<DocumentReadModel> store) : base(store)
        {
        }

        public async Task<IEnumerable<DocumentReadModel>> ExecuteQueryAsync(GetAllDocumentsInCategoryQuery query,
            CancellationToken cancellationToken)
        {
            var roles = query.Roles;
            var result =
                await store.FindAsync(
                    rm => rm.Category == query.Category && rm.Roles.Any(name => roles.Contains(name)) && rm.IsVisible &&
                          !rm.MarkedForDeletion,
                    cancellationToken: cancellationToken);

            return await result.ToListAsync(cancellationToken);
        }
    }
}