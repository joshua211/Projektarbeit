using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HT.Application.Documents;
using HT.Application.Documents.Interfaces;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Shared.Documents
{
    partial class DocumentAppBar
    {
        private IEnumerable<SearchableDocument> documents;
        private ServiceResult serviceResult;
        private UserContext userContext;

        [Parameter] public Func<Task<ServiceResult<IEnumerable<DocumentReadModel>>>> LoadDocumentsAsync { get; set; }
        [Parameter] public string AppbarTitle { get; set; }
        [Parameter] public bool ShowEditorContext { get; set; }
        [Parameter] public bool ShowReturnButton { get; set; }
        [Parameter] public RenderFragment NoContentView { get; set; }

        [Inject] public IContextProvider ContextProvider { get; set; }
        [Inject] public IDocumentService DocumentService { get; set; }
        [Inject] public IMatToaster Toaster { get; set; }
        [Inject] public IMatDialogService Dialog { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected override async Task OnInitializedAsync()
        {
            userContext = ContextProvider.GetUserContext();
            await SetDocuments();
        }

        private async Task SetDocuments()
        {
            var result = await LoadDocumentsAsync();
            serviceResult = result;
            if (result.WasSuccessful)
            {
                documents = result.Value.Select(rm => new SearchableDocument(rm));
            }
        }

        private void Edit(DocumentReadModel readModel)
        {
            Nav.NavigateTo("documents/edit/" + readModel.Id);
        }

        private void View(DocumentReadModel readModel)
        {
            Nav.NavigateTo("documents/view/" + readModel.Id);
        }

        private async Task Delete(DocumentReadModel readModel)
        {
            if (!await Dialog.ConfirmAsync($"{readModel.Revisions.Last().Title} löschen?")) return;

            var result = await DocumentService.DeleteDocumentAsync(new DocumentId(readModel.Id));
            if (result.WasSuccessful)
            {
                await SetDocuments();
                Toaster.Add("Dokument gelöscht", MatToastType.Info, "Erfolgreich");
            }
            else
            {
                Toaster.Add(result.ErrorMessage, MatToastType.Danger, "Error");
            }

            StateHasChanged();
        }

        private async Task Restore(DocumentReadModel readModel)
        {
            if (!await Dialog.ConfirmAsync($"{readModel.Revisions.Last().Title} wiederherstellen?")) return;

            var result =
                await DocumentService.RestoreDocumentAsync(new DocumentId(readModel.Id), CancellationToken.None);
            if (result.WasSuccessful)
            {
                await SetDocuments();
                Toaster.Add("Dokument wiederhergestellt", MatToastType.Info, "Erfolgreich");
            }
            else
            {
                Toaster.Add(result.ErrorMessage, MatToastType.Danger, "Error");
            }

            StateHasChanged();
        }

        private async Task ChangeVisibility(DocumentReadModel readModel)
        {
            if (!await Dialog.ConfirmAsync(readModel.IsVisible
                ? "Dokument auf unsichtbar setzen?"
                : "Dokument auf sichtbar setzen?"))
                return;

            var result = await DocumentService.SetVisibilityAsync(new DocumentId(readModel.Id),
                !readModel.IsVisible,
                CancellationToken.None);
            if (result.WasSuccessful)
            {
                readModel.IsVisible = !readModel.IsVisible;
                Toaster.Add("Sichtbarkeit wurde geändert", MatToastType.Info, icon: MatIconNames.Visibility);
            }
            else
            {
                Toaster.Add("Sichtbarkeit konnte nicht gesetzt werden: " + result.ErrorMessage, MatToastType.Danger,
                    "Error");
            }
        }
    }
}