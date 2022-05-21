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
    public class GetArticlesInRangeQueryHandler : ArticleQueryHandler,
        IQueryHandler<GetArticlesInRangeQuery, IEnumerable<ArticleReadModel>>
    {
        public GetArticlesInRangeQueryHandler(IMongoDbReadModelStore<ArticleReadModel> store) : base(store)
        {
        }

        public async Task<IEnumerable<ArticleReadModel>> ExecuteQueryAsync(GetArticlesInRangeQuery query,
            CancellationToken cancellationToken)
        {
            var result = await store.FindAsync(rm => rm.CreationTime <= query.End && rm.CreationTime >= query.Start,
                cancellationToken: cancellationToken);

            return await result.ToListAsync(cancellationToken);
        }
    }
}