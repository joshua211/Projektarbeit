using EventFlow.Queries;
using HT.Core.Aggregates.Article;

namespace HT.Application.Article.Queries
{
    public class GetArticleByIdQuery : IQuery<ArticleReadModel>
    {
        public GetArticleByIdQuery(ArticleId id)
        {
            Id = id;
        }

        public ArticleId Id { get; private set; }
    }
}