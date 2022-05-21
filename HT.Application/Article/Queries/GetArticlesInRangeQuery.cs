using System;
using System.Collections.Generic;
using EventFlow.Queries;

namespace HT.Application.Article.Queries
{
    public class GetArticlesInRangeQuery : IQuery<IEnumerable<ArticleReadModel>>
    {
        public GetArticlesInRangeQuery(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
    }
}