using EventFlow.MongoDB.ReadStores;
using HT.Application.Documents;
using MongoDB.Driver;

namespace HT.Infrastructure.QueryHandlers.Documents
{
    public abstract class DocumentQueryHandler
    {
        protected readonly IMongoDbReadModelStore<DocumentReadModel> store;

        public DocumentQueryHandler(IMongoDbReadModelStore<DocumentReadModel> store)
        {
            this.store = store;
        }
    }
}