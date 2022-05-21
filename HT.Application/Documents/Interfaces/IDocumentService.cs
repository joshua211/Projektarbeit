using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;

namespace HT.Application.Documents.Interfaces
{
    public interface IDocumentService
    {
        Task<ServiceResult<DocumentId>> CreateDocumentAsync(string content, string title, bool isVisible,
            IEnumerable<string> roleNames,
            string category, CancellationToken token);

        Task<ServiceResult> DeleteDocumentAsync(DocumentId id, DateTime deletionTime = default,
            CancellationToken token = default);

        Task<ServiceResult<DocumentId>> ReviseDocumentAsync(DocumentId id, string content, string title,
            IEnumerable<string> roleNames, string category, CancellationToken token);

        Task<ServiceResult<DocumentReadModel>> GetDocumentAsync(DocumentId id, CancellationToken token);

        Task<ServiceResult<IEnumerable<DocumentReadModel>>> GetAllDocumentsAsync(CancellationToken token,
            ApplicationService.ApplicationServiceContext ctx = null);

        Task<ServiceResult<IEnumerable<DocumentReadModel>>> GetDocumentsInCategoryAsync(string category,
            CancellationToken token);

        Task<ServiceResult<IEnumerable<DocumentReadModel>>> GetDocumentsForUserAsync(CancellationToken token);
        Task<ServiceResult<IEnumerable<DocumentReadModel>>> GetSubscribedDocumentsAsync(CancellationToken token);
        Task<ServiceResult<IEnumerable<string>>> GetAllCategoriesAsync(CancellationToken token);
        Task<ServiceResult> RestoreDocumentAsync(DocumentId documentId, CancellationToken token);
        Task<ServiceResult> SetVisibilityAsync(DocumentId id, bool visible, CancellationToken token);
    }
}