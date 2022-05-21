using EventFlow.MongoDB.ReadStores;
using HT.Application.Userdata;

namespace HT.Infrastructure.QueryHandlers.Userdata
{
    public abstract class UserdataQueryHandler
    {
        protected readonly IMongoDbReadModelStore<UserdataReadModel> store;

        public UserdataQueryHandler(IMongoDbReadModelStore<UserdataReadModel> store)
        {
            this.store = store;
        }
    }
}