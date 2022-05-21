using System.Threading;
using System.Threading.Tasks;
using HT.Application.Article;
using HT.Application.Article.Interfaces;
using HT.Application.Shared;
using HT.Core.Aggregates.Article;
using HT.Core.Aggregates.Document;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Pages.News
{
    partial class ArticleView
    {
        [Parameter] public string ArticleId { get; set; }
        
        [Inject] public IContextProvider ContextProvider { get; set; }
        [Inject] public IArticleService Service { get; set; }
        [Inject] public IMatDialogService DialogService { get; set; }
        [Inject] public IMatToaster Toaster { get; set; }
        [Inject] public NavigationManager Nav { get; set; }
        
        private ArticleReadModel readModel;
        private ServiceResult serviceResult;
        
        protected override async Task OnParametersSetAsync()
        {
            var result = await Service.GetArticleAsync(new ArticleId(ArticleId), CancellationToken.None);
            if (result.WasSuccessful)
            {
                readModel = result.Value;
                serviceResult = result;
            }
        }

        private async Task Delete()
        {
            var result = await DialogService.ConfirmAsync("Dokument löschen?");
            if (result)
            {
                var deleteResult =
                    await Service.DeleteArticleAsync(new ArticleId(ArticleId));
                if (deleteResult.WasSuccessful)
                {
                    Toaster.Add("Artikel wurde erfolgreich gelöscht", MatToastType.Dark);
                    Nav.NavigateTo("documents/user");
                }
                else
                {
                    Toaster.Add("Artikel konnte nicht gelöscht werden: " + deleteResult.ErrorMessage,
                        MatToastType.Dark);
                }
            }
        }

        private async Task ChangeVisibility()
        {
            var result = await Service.ChangeVisibilityAsync(new ArticleId(ArticleId), !readModel.IsVisible);
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

        private void Edit()
        {
            Nav.NavigateTo("articles/edit/" + readModel.Id);
        }
    }
}