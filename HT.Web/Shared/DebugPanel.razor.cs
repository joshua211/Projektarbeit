using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using HT.Application.Article.Interfaces;
using HT.Application.Documents.Interfaces;
using HT.Application.Shared;
using HT.Application.Userdata;
using HT.Core.Aggregates.Document;
using HT.Core.Aggregates.Document.Commands;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Shared
{
    partial class DebugPanel
    {
        private static Random random = new Random();
        private List<string> appRoles = new List<string>();
        private string displayName;
        private Guid id;
        private bool isActionInProgress;

        private bool showDrawer;
        private UserdataReadModel userdata;
        private List<string> userRoles = new List<string>();
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Inject] public IGraphService GraphService { get; set; }
        [Inject] public IContextProvider ContextProvider { get; set; }
        [Inject] public NavigationManager Nav { get; set; }
        [Inject] public IMatDialogService Dialog { get; set; }
        [Inject] public IMatToaster Toaster { get; set; }
        [Inject] public IDocumentService DocumentService { get; set; }
        [Inject] public IArticleService ArticleService { get; set; }
        [Inject] public ICommandBus CommandBus { get; set; }

        protected override async Task OnInitializedAsync()
        {
            appRoles = (await GraphService.GetAllRolesAsync()).Select(az => az.DisplayName).ToList();
            var context = ContextProvider.GetUserContext();
            userRoles = context.Roles;
            displayName = context.DisplayName;
            id = context.Id;
            userdata = context.Userdata;
        }

        private void OpenDrawer() => showDrawer = true;

        private void ReloadPage() => Nav.NavigateTo(Nav.Uri, true);

        private void NavigateTo(string path) => Nav.NavigateTo(path);

        private void RealoadCurrentPage()
        {
            var current = Nav.Uri;
            Nav.NavigateTo("/redirect");
            Nav.NavigateTo(current);
        }

        private async Task NavigationDialog(string path)
        {
            var result = await Dialog.PromptAsync("Wert eingeben");
            NavigateTo(path + result);
        }

        private async Task CreateDocumentsDialog()
        {
            var result = await Dialog.PromptAsync("Anzahl an Dokumenten", "10");
            isActionInProgress = true;
            StateHasChanged();
            try
            {
                var amount = Convert.ToInt32(result);
                var tasks = new List<Task>();
                var category = RandomString(20);
                for (int i = 0; i < amount; i++)
                {
                    tasks.Add(DocumentService.CreateDocumentAsync(RandomString(1000),
                        RandomString(50),
                        true,
                        appRoles,
                        category,
                        CancellationToken.None
                    ));
                    if (i % 5 == 0)
                        category = RandomString(20);
                }

                await Task.WhenAll(tasks);
                Toaster.Add($"{amount} Dokumente wurden erstellt", MatToastType.Success, "Success");
            }
            catch (Exception e)
            {
                Toaster.Add(e.Message, MatToastType.Warning, "Error");
            }
            finally
            {
                isActionInProgress = false;
            }
        }

        public async Task CreateArticlesDialog()
        {
            var result = await Dialog.PromptAsync("Anzahl an Artikeln", "10");
            isActionInProgress = true;
            StateHasChanged();
            try
            {
                var amount = Convert.ToInt32(result);
                var tasks = new List<Task>();
                var category = RandomString(20);
                for (int i = 0; i < amount; i++)
                {
                    tasks.Add(ArticleService.CreateArticleAsync(RandomString(1000),
                        RandomString(50),
                        true,
                        CancellationToken.None
                    ));
                }

                await Task.WhenAll(tasks);
                Toaster.Add($"{amount} Artikel wurden erstellt", MatToastType.Success, "Success");
            }
            catch (Exception e)
            {
                Toaster.Add(e.Message, MatToastType.Warning, "Error");
            }
            finally
            {
                isActionInProgress = false;
            }
        }

        private async Task MarkAllForDeletion()
        {
            isActionInProgress = true;
            StateHasChanged();
            var result = await DocumentService.GetAllDocumentsAsync(CancellationToken.None);
            if (!result.WasSuccessful)
            {
                Toaster.Add(result.ErrorMessage, MatToastType.Danger, "Error");
                return;
            }

            var tasks = new List<Task>();
            var docs = result.Value.Where(rm => !rm.MarkedForDeletion).ToList();
            foreach (var doc in docs)
            {
                tasks.Add(DocumentService.DeleteDocumentAsync(new DocumentId(doc.Id), DateTime.Now));
            }

            await Task.WhenAll(tasks);
            Toaster.Add($"{docs.Count} Dokumente wurden gelöscht", MatToastType.Success, "Success");
            isActionInProgress = false;
        }

        private async Task ClearBin()
        {
            isActionInProgress = true;
            StateHasChanged();

            var result = await DocumentService.GetAllDocumentsAsync(CancellationToken.None);
            if (!result.WasSuccessful)
            {
                Toaster.Add(result.ErrorMessage, MatToastType.Danger, "Error");
                return;
            }

            var tasks = new List<Task>();
            var docsToDelete = result.Value.Where(rm => rm.MarkedForDeletion).ToList();
            foreach (var doc in docsToDelete)
            {
                tasks.Add(CommandBus.PublishAsync(new DeleteDocumentCommand(new DocumentId(doc.Id)),
                    CancellationToken.None));
            }

            try
            {
                await Task.WhenAll(tasks);
                Toaster.Add($"{docsToDelete.Count} Dokumente wurden permanent gelöscht", MatToastType.Success,
                    "Success");
            }
            catch (Exception e)
            {
                Toaster.Add(e.Message, MatToastType.Danger, "Error");
            }
            finally
            {
                isActionInProgress = false;
            }
        }

        private void UseSettings()
        {
            var ctx = new UserContext(id, displayName, userRoles);
            ctx.Userdata = userdata;
            ContextProvider.SetUserContext(ctx);
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}