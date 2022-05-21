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
    public class GetDocumentsForUserQueryHandler : DocumentQueryHandler,
        IQueryHandler<GetDocumentsForUserQuery, IEnumerable<DocumentReadModel>>
    {
        public GetDocumentsForUserQueryHandler(IMongoDbReadModelStore<DocumentReadModel> store) : base(store)
        {
        }

        public async Task<IEnumerable<DocumentReadModel>> ExecuteQueryAsync(GetDocumentsForUserQuery query,
            CancellationToken cancellationToken)
        {
            var roles = query.Roles;
            var documentsByRole = await store.FindAsync(
                rm => rm.Roles.Any(name => roles.Contains(name)) && !rm.MarkedForDeletion && rm.IsVisible,
                cancellationToken: cancellationToken);

            return await documentsByRole.ToListAsync(cancellationToken);
        }
    }
}