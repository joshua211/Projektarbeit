using EventFlow.Core;

namespace HT.Core.Aggregates.Article
{
    public class ArticleId : Identity<ArticleId>
    {
        public ArticleId(string value) : base(value)
        {
        }
    }
}