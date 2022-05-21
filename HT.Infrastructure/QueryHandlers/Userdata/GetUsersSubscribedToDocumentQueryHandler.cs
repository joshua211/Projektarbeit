using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using HT.Application.Userdata;
using HT.Application.Userdata.Queries;
using MongoDB.Driver;

namespace HT.Infrastructure.QueryHandlers.Userdata
{
    public class GetUsersSubscribedToDocumentQueryHandler :UserdataQueryHandler, IQueryHandler<GetUsersSubscribedToDocumentQuery, IEnumerable<UserdataReadModel>>
    {
        public GetUsersSubscribedToDocumentQueryHandler(IMongoDbReadModelStore<UserdataReadModel> store) : base(store)
        {
        }

        public async Task<IEnumerable<UserdataReadModel>> ExecuteQueryAsync(GetUsersSubscribedToDocumentQuery query, CancellationToken cancellationToken)
        {
            var result = await store.FindAsync(rm => rm.SubscribedDocuments.Contains(query.Id.Value),
                cancellationToken: cancellationToken);

            return await result.ToListAsync(cancellationToken);
        }
    }
}