using System.Collections.Generic;
using HT.Application.Article;

namespace HT.Web.Shared.Articles
{
    public class SearchableArticle : ISearchableContent<ArticleReadModel>
    {
        private readonly ArticleReadModel readModel;

        public SearchableArticle(ArticleReadModel readModel)
        {
            this.readModel = readModel;
        }

        public IEnumerable<string> GetSearchableKeywords()
        {
            return new[] {readModel.CurrentRevision.Title};
        }

        public ArticleReadModel GetContent() => readModel;
    }
}