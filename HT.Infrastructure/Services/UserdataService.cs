using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using HT.Application.Shared;
using HT.Application.Userdata;
using HT.Application.Userdata.Interfaces;
using HT.Application.Userdata.Queries;
using HT.Core.Aggregates.Document;
using HT.Core.Aggregates.Userdata;
using HT.Core.Aggregates.Userdata.Commands;
using Serilog;

namespace HT.Infrastructure.Services
{
    public class UserdataService : ApplicationService, IUserdataService
    {
        public UserdataService(ILogger logger, IQueryProcessor queryProcessor, ICommandBus commandBus) : base(logger,
            queryProcessor, commandBus)
        {
        }

        public async Task<ServiceResult<UserdataReadModel>> GetUserdataAsync(UserContext context,
            CancellationToken token)
        {
            using var ctx = TransactionBegin();
            try
            {
                var result = await ProcessAsync(ctx, new GetUserdataForUserQuery(context), token);
                if (result is null)
                {
                    TransactionError(ctx, "No user data found", null);
                    return ServiceResult<UserdataReadModel>.Failure("No userdata found");
                }

                return ServiceResult<UserdataReadModel>.Success(result);
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult<UserdataReadModel>.Failure(e.Message);
            }
        }

        public async Task<ServiceResult<UserdataReadModel>> CreateUserdataAsync(UserContext context,
            CancellationToken token)
        {
            using var ctx = TransactionBegin();
            try
            {
                var user = UserContext.ToUser(context);
                await PublishAsync(ctx, new CreateUserdataCommand(UserdataId.New, user), token);

                var result = await GetUserdataAsync(context, token);
                if (!result.WasSuccessful)
                {
                    TransactionError(ctx, "Failed to get userdata for user: " + context.DisplayName, null);
                    return ServiceResult<UserdataReadModel>.Failure("Failed to get userdata");
                }

                return result;
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult<UserdataReadModel>.Failure(e.Message);
            }
        }

        public async Task<ServiceResult> SubscribeToDocumentAsync(UserdataId userdataId, DocumentId documentId,
            CancellationToken token)
        {
            using var ctx = TransactionBegin();
            try
            {
                var result = await PublishAsync(ctx, new SubscribeToDocumentCommand(userdataId, documentId), token);
                if (!result.IsSuccess)
                {
                    TransactionError(ctx, "Failed to subscribe user to document", null);
                    return ServiceResult.Failure("Failed to subscribe to document");
                }

                return ServiceResult.Success();
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult.Failure(e.Message);
            }
        }

        public async Task<ServiceResult> UnsubscribeToDocumentAsync(UserdataId userdataId, DocumentId documentId,
            CancellationToken token)
        {
            using var ctx = TransactionBegin();
            try
            {
                var result = await PublishAsync(ctx, new UnsubscribeToDocumentCommand(userdataId, documentId), token);
                if (!result.IsSuccess)
                {
                    TransactionError(ctx, "Failed to unsubscribe to document: " + documentId.ToString(), null);
                    return ServiceResult.Failure("Failed to unsubscribe to document");
                }

                return ServiceResult.Success();
            }
            catch (Exception e)
            {
                TransactionError(ctx, e);
                return ServiceResult.Failure(e.Message);
            }
        }
    }
}