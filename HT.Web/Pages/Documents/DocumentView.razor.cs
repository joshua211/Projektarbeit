using System.Threading;
using System.Threading.Tasks;
using HT.Application.Documents;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Pages.Documents
{
    partial class DocumentView
    {
        private DocumentReadModel readModel;
        private ServiceResult serviceResult;
        [Parameter] public string DocumentId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var result = await service.GetDocumentAsync(new DocumentId(DocumentId), CancellationToken.None);
            if (result.WasSuccessful)
            {
                readModel = result.Value;
                serviceResult = result;
            }
        }

        private async Task Delete()
        {
            var result = await dialogService.ConfirmAsync("Dokument löschen?");
            if (result)
            {
                var deleteResult =
                    await service.DeleteDocumentAsync(new DocumentId(readModel.Id));
                if (deleteResult.WasSuccessful)
                {
                    toaster.Add("Dokument wurde erfolgreich gelöscht", MatToastType.Dark);
                    nav.NavigateTo("documents/user");
                }
                else
                {
                    toaster.Add("Dokument konnte nicht gelöscht werden: " + deleteResult.ErrorMessage,
                        MatToastType.Dark);
                }
            }
        }

        private async Task ChangeVisibility()
        {
            var result = await service.SetVisibilityAsync(new DocumentId(readModel.Id), !readModel.IsVisible,
                CancellationToken.None);
            if (result.WasSuccessful)
            {
                readModel.IsVisible = !readModel.IsVisible;
                toaster.Add("Sichtbarkeit wurde geändert", MatToastType.Info, icon: MatIconNames.Visibility);
            }
            else
            {
                toaster.Add("Sichtbarkeit konnte nicht gesetzt werden: " + result.ErrorMessage, MatToastType.Danger,
                    "Error");
            }
        }

        private void Edit()
        {
            nav.NavigateTo("documents/edit/" + readModel.Id);
        }
    }
}