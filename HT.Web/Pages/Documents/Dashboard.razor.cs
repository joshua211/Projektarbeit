using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Documents;
using HT.Application.Documents.Interfaces;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using HT.Web.Shared.Documents;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Pages.Documents
{
    partial class Dashboard
    {
        [Inject] public IDocumentService documentService { get; set; }

        private async Task<ServiceResult<IEnumerable<DocumentReadModel>>> LoadDocuments()
        {
            return await documentService.GetSubscribedDocumentsAsync(CancellationToken.None);
        }
    }
}