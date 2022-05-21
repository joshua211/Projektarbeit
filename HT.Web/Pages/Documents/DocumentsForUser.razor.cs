using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Documents;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using HT.Web.Shared.Documents;

namespace HT.Web.Pages.Documents
{
    partial class DocumentsForUser
    {
        private async Task<ServiceResult<IEnumerable<DocumentReadModel>>> LoadDocuments()
        {
            return await documentService.GetDocumentsForUserAsync(CancellationToken.None);
        }
    }
}