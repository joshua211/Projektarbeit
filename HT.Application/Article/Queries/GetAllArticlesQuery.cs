using System.Collections.Generic;
using EventFlow.Queries;

namespace HT.Application.Article.Queries
{
    public class GetAllArticlesQuery : IQuery<IEnumerable<ArticleReadModel>>
    {
    }
}