using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using HT.Application.Article;
using HT.Application.Article.Interfaces;
using HT.Application.Article.Models;
using HT.Application.Article.Queries;
using HT.Application.Shared;
using HT.Core.Aggregates.Article;
using HT.Core.Aggregates.Article.Commands;
using HT.Core.Shared.ValueObjects;
using HT.Infrastructure.Shared;
using Serilog;

namespace HT.Infrastructure.Services
{
    public class ArticleService : ServiceWithUserContext, IArticleService
    {
        public ArticleService(IContextProvider provider, ILogger logger, ICommandBus bus, IQueryProcessor processor) :
            base(provider, logger, bus, processor)
        {
        }

        public async Task<ServiceResult<ArticleId>> CreateArticleAsync(string content, string title, bool isVisible,
            CancellationToken token = default)
        {
            using var ctx = TransactionBegin();
            try
            {
                var id = ArticleId.New;
                var user = UserContext.ToUser(Context);
                var command = new CreateArticleCommand(id,
                    new Revision(user, new Title(title), DateTime.Now, content), new Visibility(isVisible));
                var result = await PublishAsync(ctx, command, token);

                if (!result.IsSuccess)
                {
                    TransactionWarning(ctx, "Failed to create Article with ID: {ID}", id.ToString());

                    return ServiceResult<ArticleId>.Failure("Something went wrong while trying to create the article");
                }

                return ServiceResult<ArticleId>.Success(id);
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult<ArticleId>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult> DeleteArticleAsync(ArticleId id, CancellationToken token = default)
        {
            using var ctx = TransactionBegin();
            try
            {
                var user = UserContext.ToUser(Context);
                var command = new DeleteArticleCommand(id, user);

                var result = await PublishAsync(ctx, command, token);
                if (!result.IsSuccess)
                {
                    TransactionWarning(ctx, "Failed to delete Article with ID: {ID}", id);

                    return ServiceResult.Failure("Failed to delete Article");
                }

                return ServiceResult.Success();
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult> UpdateArticleAsync(ArticleId id, ArticleRevision revision,
            CancellationToken token = default)
        {
            using var ctx = TransactionBegin();
            try
            {
                var user = UserContext.ToUser(Context);
                var command = new UpdateArticleCommand(id,
                    new Revision(user, new Title(revision.Title), DateTime.Now, revision.Content));

                var result = await PublishAsync(ctx, command, token);
                if (!result.IsSuccess)
                {
                    TransactionWarning(ctx, "Failed to update Article with ID: {ID}", id);

                    return ServiceResult.Failure("Failed to update Article");
                }

                return ServiceResult.Success();
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult> ChangeVisibilityAsync(ArticleId id, bool isVisible,
            CancellationToken token = default)
        {
            using var ctx = TransactionBegin();
            try
            {
                var user = UserContext.ToUser(Context);
                var command = new ChangeVisibilityCommand(id, new Visibility(isVisible), user);

                var result = await PublishAsync(ctx, command, token);
                if (!result.IsSuccess)
                {
                    TransactionWarning(ctx, "Failed to change visibility to {IsVisible} for Article with ID: {ID}",
                        isVisible, id);

                    return ServiceResult.Failure("Failed to change Visibility");
                }

                return ServiceResult.Success();
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<ArticleReadModel>> GetArticleAsync(ArticleId id,
            CancellationToken token = default)
        {
            using var ctx = TransactionBegin();
            try
            {
                var query = new GetArticleByIdQuery(id);

                var result = await ProcessAsync(ctx, query, token);
                if (result is null)
                {
                    TransactionWarning(ctx, "Failed to get Article with ID: {ID}", id);

                    return ServiceResult<ArticleReadModel>.Failure("Failed to get Article");
                }

                return ServiceResult<ArticleReadModel>.Success(result);
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult<ArticleReadModel>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ArticleReadModel>>> GetAllArticlesAsync(
            CancellationToken token = default)
        {
            using var ctx = TransactionBegin();
            try
            {
                var query = new GetAllArticlesQuery();

                var result = await ProcessAsync(ctx, query, token);

                return ServiceResult<IEnumerable<ArticleReadModel>>.Success(result);
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult<IEnumerable<ArticleReadModel>>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ArticleReadModel>>> GetArticlesInRangeAsync(DateTime start,
            DateTime end, CancellationToken token = default)
        {
            using var ctx = TransactionBegin();
            try
            {
                var query = new GetArticlesInRangeQuery(start, end);

                var result = await ProcessAsync(ctx, query, token);

                return ServiceResult<IEnumerable<ArticleReadModel>>.Success(result);
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult<IEnumerable<ArticleReadModel>>.Failure("Failed: " + e.Message);
            }
        }
    }
}