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
    public class GetDocumentByIdQueryHandler : DocumentQueryHandler, IQueryHandler<GetDocumentByIdQuery, DocumentReadModel>
    {
        public GetDocumentByIdQueryHandler(IMongoDbReadModelStore<DocumentReadModel> store) : base(store)
        {
        }

        public async Task<DocumentReadModel> ExecuteQueryAsync(GetDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            var idString = query.Id.ToString();
            var result = await store.FindAsync(rm => rm.Id == idString, cancellationToken: cancellationToken);

            return await result.FirstOrDefaultAsync(cancellationToken);
        }
    }
}