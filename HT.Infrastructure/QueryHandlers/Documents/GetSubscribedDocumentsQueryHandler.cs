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
    public class GetSubscribedDocumentsQueryHandler : DocumentQueryHandler,
        IQueryHandler<GetSubscribedDocumentsQuery, IEnumerable<DocumentReadModel>>
    {
        public GetSubscribedDocumentsQueryHandler(IMongoDbReadModelStore<DocumentReadModel> store) : base(store)
        {
        }

        public async Task<IEnumerable<DocumentReadModel>> ExecuteQueryAsync(GetSubscribedDocumentsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await store.FindAsync(rm =>
                    query.SubscribedIds.Contains(rm.Id) && rm.Roles.Any(role => query.UserRoles.Contains(role)) &&
                    rm.IsVisible && !rm.MarkedForDeletion,
                cancellationToken: cancellationToken);

            return await result.ToListAsync(cancellationToken);
        }
    }
}