using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazored.TextEditor;
using HT.Application.Shared;
using HT.Core.Aggregates.Document;
using HT.Web.Shared.Documents;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HT.Web.Pages.Documents
{
    partial class DocumentEdit
    {
        private string appbarTitle = "Dokument erstellen";
        private IReadOnlyList<string> categories = new List<string>();
        private DocumentEditModel editModel = new DocumentEditModel();
        private bool editorInitialized;
        private BlazoredTextEditor htmlEditor;
        private bool isPending;
        private List<string> roles = new List<string>();
        private bool useExistingCategories;
        [Parameter] public string ReadModelId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!contextProvider.GetUserContext().IsEditor)
                nav.NavigateTo("documents");
            await Task.WhenAll(LoadCategories(), LoadRoles());
        }

        private async Task LoadCategories()
        {
            var result = await documentService.GetAllCategoriesAsync(CancellationToken.None);
            if (result.WasSuccessful)
            {
                categories = result.Value.ToList();
                if (categories.Any())
                    useExistingCategories = true;
            }
        }

        private async Task LoadRoles()
        {
            roles = (await graphService.GetAllRolesAsync()).Where(r => !r.DisplayName.Contains("HT"))
                .Select(r => r.DisplayName).ToList();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(ReadModelId))
            {
                var result =
                    await documentService.GetDocumentAsync(new DocumentId(ReadModelId), CancellationToken.None);
                if (result.WasSuccessful)
                {
                    var rev = result.Value.Revisions.Last();
                    editModel.Category = result.Value.Category;
                    editModel.Title = rev.Title;
                    editModel.Content = rev.Content;
                    editModel.Roles = result.Value.Roles;

                    appbarTitle = "Dokument bearbeiten";
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                return;
            if (!editorInitialized)
            {
                await SetHtmlContent(editModel.Content);
                editorInitialized = true;
            }
        }

        private async Task<string> GetHtmlContent()
        {
            return await htmlEditor.GetHTML();
        }

        private async Task SetHtmlContent(string content)
        {
            await htmlEditor.LoadHTMLContent(content);
            StateHasChanged();
        }

        private async Task OnValidSubmit()
        {
            isPending = true;
            editModel.Content = await GetHtmlContent();
            StateHasChanged();

            ServiceResult<DocumentId> result;
            if (string.IsNullOrEmpty(ReadModelId))
            {
                result = await documentService.CreateDocumentAsync(editModel.Content, editModel.Title,
                    editModel.IsVisible,
                    editModel.Roles, editModel.Category, CancellationToken.None);
                if (result.WasSuccessful)
                    toaster.Add("Dokument wurde erfolgreich erstellt", MatToastType.Success,
                        "Erfolgreich erstellt");
                else
                    toaster.Add("Dokument konnte nicht erstellt werden: " + result.ErrorMessage,
                        MatToastType.Danger, "Fehler beim Erstellen");
            }
            else
            {
                result = await documentService.ReviseDocumentAsync(new DocumentId(ReadModelId), editModel.Content,
                    editModel.Title, editModel.Roles, editModel.Category, CancellationToken.None);
                if (result.WasSuccessful)
                    toaster.Add("Dokument wurde erfolgreich überarbeitet", MatToastType.Success,
                        "Erfolgreich überarbeitet");
                else
                    toaster.Add("Dokument konnte nicht überarbeitet werden: " + result.ErrorMessage,
                        MatToastType.Danger);
            }

            if (result.WasSuccessful)
                nav.NavigateTo("documents/view/" + result.Value.Value);
        }

        private async Task Back()
        {
            await js.InvokeVoidAsync("navigateBack");
        }
    }
}