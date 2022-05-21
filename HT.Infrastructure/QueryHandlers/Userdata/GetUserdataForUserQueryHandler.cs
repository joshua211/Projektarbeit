using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using HT.Application.Userdata;
using HT.Application.Userdata.Queries;
using MongoDB.Driver;

namespace HT.Infrastructure.QueryHandlers.Userdata
{
    public class GetUserdataForUserQueryHandler : UserdataQueryHandler , IQueryHandler<GetUserdataForUserQuery, UserdataReadModel>
    {
        public GetUserdataForUserQueryHandler(IMongoDbReadModelStore<UserdataReadModel> store) : base(store)
        {
        }


        public async Task<UserdataReadModel> ExecuteQueryAsync(GetUserdataForUserQuery query, CancellationToken cancellationToken)
        {
            var result = await store.FindAsync(rm => rm.UserId == query.Context.Id, cancellationToken: cancellationToken);

            return result.FirstOrDefault(cancellationToken);
        }
    }
}