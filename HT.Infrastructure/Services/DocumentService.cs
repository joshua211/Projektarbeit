using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using HT.Application.Documents;
using HT.Application.Documents.Interfaces;
using HT.Application.Documents.Queries;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using HT.Core.Aggregates.Document.Commands;
using HT.Core.Aggregates.Document.ValueObjects;
using HT.Core.Shared.ValueObjects;
using HT.Infrastructure.Shared;
using Serilog;

namespace HT.Infrastructure.Services
{
    public class DocumentService : ServiceWithUserContext, IDocumentService
    {
        public DocumentService(IContextProvider provider, IQueryProcessor queryProcessor, ICommandBus bus,
            ILogger logger) :
            base(provider, logger, bus, queryProcessor)
        {
        }

        public async Task<ServiceResult<DocumentId>> CreateDocumentAsync(string content, string title, bool isVisible,
            IEnumerable<string> roleNames,
            string category, CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var id = DocumentId.New;

                var user = UserContext.ToUser(Context);
                var roles = roleNames.Select(name => new Role(name)).ToList();

                var result = await PublishAsync(context, new CreateDocumentCommand(id,
                    new Revision(user, new Title(title), DateTime.Now, content),
                    new Visibility(isVisible),
                    user,
                    roles,
                    new Category(category)), token);

                if (!result.IsSuccess)
                {
                    TransactionWarning(context, "Failed to create Document with ID: {ID}", id.ToString());

                    return ServiceResult<DocumentId>.Failure(
                        "Something went wrong while trying to create the document");
                }

                return ServiceResult<DocumentId>.Success(id);
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult<DocumentId>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult> DeleteDocumentAsync(DocumentId id, DateTime deletionTime = default,
            CancellationToken token = default)
        {
            using var context = TransactionBegin();
            try
            {
                deletionTime = deletionTime == default ? DateTime.Now.AddDays(7) : deletionTime;
                var user = UserContext.ToUser(Context);

                var result = await PublishAsync(context,
                    new MarkDocumentForDeletionCommand(id, new DeletionMarker(deletionTime), user), token);

                if (!result.IsSuccess)
                {
                    TransactionWarning(context, "Failed to mark Document with ID: {ID} for deletion", id.ToString());

                    return ServiceResult.Failure("Something went wrong while trying to delete the document");
                }

                return ServiceResult.Success();
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<DocumentId>> ReviseDocumentAsync(DocumentId id, string content, string title,
            IEnumerable<string> roleNames, string category, CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var user = UserContext.ToUser(Context);
                var roles = roleNames.Select(name => new Role(name)).ToList();


                var result = await PublishAsync(context, new ReviseDocumentCommand(id,
                    new Revision(user, new Title(title), DateTime.Now, content),
                    roles,
                    new Category(category)), token);

                if (!result.IsSuccess)
                {
                    TransactionWarning(context, "Failed to revise Document with ID: {ID}", id);

                    return ServiceResult<DocumentId>.Failure(
                        "Something went wrong while trying to revise the document");
                }

                return ServiceResult<DocumentId>.Success(id);
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult<DocumentId>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<DocumentReadModel>>> GetAllDocumentsAsync(CancellationToken token,
            ApplicationServiceContext ctx = null)
        {
            using var context = TransactionBegin(parentContext: ctx);
            try
            {
                var model = await ProcessAsync(context, new GetAllDocumentsQuery(), token);

                return ServiceResult<IEnumerable<DocumentReadModel>>.Success(model);
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult<IEnumerable<DocumentReadModel>>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<DocumentReadModel>>> GetDocumentsInCategoryAsync(string category,
            CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var roles = Context.Roles;

                var model = await ProcessAsync(context, new GetAllDocumentsInCategoryQuery(category, roles),
                    token);

                return ServiceResult<IEnumerable<DocumentReadModel>>.Success(model);
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult<IEnumerable<DocumentReadModel>>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<DocumentReadModel>> GetDocumentAsync(DocumentId id, CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var model = await ProcessAsync(context, new GetDocumentByIdQuery(id), token);

                if (model is null)
                {
                    TransactionWarning(context, "No ReadModel found for ID: {ID}", id.ToString());

                    return ServiceResult<DocumentReadModel>.Failure("Could not find a model width ID: " + id);
                }

                return ServiceResult<DocumentReadModel>.Success(model);
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult<DocumentReadModel>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<DocumentReadModel>>> GetDocumentsForUserAsync(
            CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var roles = Context.Roles;

                var model = await ProcessAsync(context, new GetDocumentsForUserQuery(roles), token);

                return ServiceResult<IEnumerable<DocumentReadModel>>.Success(model);
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult<IEnumerable<DocumentReadModel>>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<DocumentReadModel>>> GetSubscribedDocumentsAsync(
            CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var roles = Context.Roles;
                var ids = Context.Userdata.SubscribedDocuments;

                var result = await ProcessAsync(context, new GetSubscribedDocumentsQuery(ids, roles), token);

                return ServiceResult<IEnumerable<DocumentReadModel>>.Success(result);
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult<IEnumerable<DocumentReadModel>>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<string>>> GetAllCategoriesAsync(CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var result = await GetAllDocumentsAsync(token, context);

                if (result.WasSuccessful)
                {
                    return ServiceResult<IEnumerable<string>>.Success(result.Value.Select(rm => rm.Category)
                        .Distinct());
                }

                TransactionError(context, "Failed to get all categories", null);
                return ServiceResult<IEnumerable<string>>.Failure(result.ErrorMessage);
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult<IEnumerable<string>>.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult> RestoreDocumentAsync(DocumentId documentId, CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var user = UserContext.ToUser(Context);
                var result = await PublishAsync(context, new RestoreDocumentCommand(documentId, user), token);

                if (!result.IsSuccess)
                {
                    TransactionWarning(context, "Could not restore Document with ID: {ID}", documentId.ToString());

                    return ServiceResult.Failure("Something went wrong while trying to restore the document");
                }

                return ServiceResult.Success();
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult.Failure("Failed: " + e.Message);
            }
        }

        public async Task<ServiceResult> SetVisibilityAsync(DocumentId id, bool visible, CancellationToken token)
        {
            using var context = TransactionBegin();
            try
            {
                var user = UserContext.ToUser(Context);
                var result = await PublishAsync(context, new ChangeVisibilityCommand(id, user, new Visibility(visible)),
                    token);

                if (!result.IsSuccess)
                {
                    TransactionWarning(context, "Could not change visibility of Document with ID: {ID}", id.ToString());

                    return ServiceResult.Failure(
                        "Something went wrong while trying to change the visibility of the document");
                }

                return ServiceResult.Success();
            }
            catch (Exception e)
            {
                TransactionError(context, e);
                return ServiceResult.Failure("Failed: " + e.Message);
            }
        }
    }
}