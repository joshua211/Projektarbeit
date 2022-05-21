using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using HT.Application.Documents;
using HT.Application.Documents.Queries;
using MongoDB.Driver;

namespace HT.Infrastructure.QueryHandlers.Documents
{
    public class GetAllDocumentsQueryHandler : DocumentQueryHandler, IQueryHandler<GetAllDocumentsQuery, IEnumerable<DocumentReadModel>>
    {
        public GetAllDocumentsQueryHandler(IMongoDbReadModelStore<DocumentReadModel> store) : base(store)
        {
        }

        public async Task<IEnumerable<DocumentReadModel>> ExecuteQueryAsync(GetAllDocumentsQuery query, CancellationToken cancellationToken)
        {
            return await (await store.FindAsync(rm => true, cancellationToken: cancellationToken)).ToListAsync(cancellationToken);
        }
    }
}