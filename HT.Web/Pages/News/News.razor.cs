using System.Collections.Generic;
using System.Threading.Tasks;
using HT.Application.Article;
using HT.Application.Article.Interfaces;
using HT.Application.Shared;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Pages.News
{
    partial class News
    {
        [Inject] public IArticleService ArticleService { get; set; }

        private async Task<ServiceResult<IEnumerable<ArticleReadModel>>> LoadArticles()
        {
            return await ArticleService.GetAllArticlesAsync();
        }
    }
}