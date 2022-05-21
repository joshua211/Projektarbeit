using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using HT.Application.Article;
using HT.Application.Article.Queries;
using MongoDB.Driver;

namespace HT.Infrastructure.QueryHandlers.Article
{
    public class GetArticleByIdQueryHandler : ArticleQueryHandler,
        IQueryHandler<GetArticleByIdQuery, ArticleReadModel>
    {
        public GetArticleByIdQueryHandler(IMongoDbReadModelStore<ArticleReadModel> store) : base(store)
        {
        }

        public async Task<ArticleReadModel> ExecuteQueryAsync(GetArticleByIdQuery query,
            CancellationToken cancellationToken)
        {
            var result = await store.FindAsync(rm => rm.Id == query.Id.Value, cancellationToken: cancellationToken);

            return await result.FirstOrDefaultAsync(cancellationToken);
        }
    }
}