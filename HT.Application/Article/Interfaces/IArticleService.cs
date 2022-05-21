using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Article.Models;
using HT.Application.Shared;
using HT.Core.Aggregates.Article;

namespace HT.Application.Article.Interfaces
{
    public interface IArticleService
    {
        Task<ServiceResult<ArticleId>> CreateArticleAsync(string content, string title, bool isVisible,
            CancellationToken token);

        Task<ServiceResult> DeleteArticleAsync(ArticleId id, CancellationToken token = default);

        Task<ServiceResult> UpdateArticleAsync(ArticleId id, ArticleRevision revision,
            CancellationToken token = default);

        Task<ServiceResult> ChangeVisibilityAsync(ArticleId id, bool isVisible, CancellationToken token = default);

        Task<ServiceResult<ArticleReadModel>> GetArticleAsync(ArticleId id, CancellationToken token = default);

        Task<ServiceResult<IEnumerable<ArticleReadModel>>> GetAllArticlesAsync(CancellationToken token = default);

        Task<ServiceResult<IEnumerable<ArticleReadModel>>> GetArticlesInRangeAsync(DateTime start, DateTime end,
            CancellationToken token = default);
    }
}