using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Documents;
using HT.Application.Shared;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Pages.Documents
{
    partial class DocumentsForCategory
    {
        [Parameter] public string Category { get; set; }

        private async Task<ServiceResult<IEnumerable<DocumentReadModel>>> LoadDocuments()
        {
            return await documentService.GetDocumentsInCategoryAsync(Category, CancellationToken.None);
        }
    }
}