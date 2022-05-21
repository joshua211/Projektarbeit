using EventFlow.MongoDB.ReadStores;
using HT.Application.Article;

namespace HT.Infrastructure.QueryHandlers.Article
{
    public class ArticleQueryHandler
    {
        protected readonly IMongoDbReadModelStore<ArticleReadModel> store;

        public ArticleQueryHandler(IMongoDbReadModelStore<ArticleReadModel> store)
        {
            this.store = store;
        }
    }
}