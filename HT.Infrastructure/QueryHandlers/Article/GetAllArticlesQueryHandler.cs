using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using HT.Application.Article;
using HT.Application.Article.Queries;
using MongoDB.Driver;

namespace HT.Infrastructure.QueryHandlers.Article
{
    public class GetAllArticlesQueryHandler : ArticleQueryHandler,
        IQueryHandler<GetAllArticlesQuery, IEnumerable<ArticleReadModel>>
    {
        public GetAllArticlesQueryHandler(IMongoDbReadModelStore<ArticleReadModel> store) : base(store)
        {
        }

        public async Task<IEnumerable<ArticleReadModel>> ExecuteQueryAsync(GetAllArticlesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await store.FindAsync(rm => true, cancellationToken: cancellationToken);

            return await result.ToListAsync(cancellationToken);
        }
    }
}