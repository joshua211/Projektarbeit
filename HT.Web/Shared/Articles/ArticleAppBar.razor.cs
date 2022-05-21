using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HT.Application.Article;
using HT.Application.Article.Interfaces;
using HT.Application.Shared;
using HT.Core.Aggregates.Article;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Shared.Articles
{
    partial class ArticleAppBar
    {
        private IEnumerable<SearchableArticle> articles;
        private ServiceResult serviceResult;
        private UserContext userContext;

        [Parameter] public Func<Task<ServiceResult<IEnumerable<ArticleReadModel>>>> LoadDocumentsAsync { get; set; }
        [Parameter] public string AppbarTitle { get; set; }
        [Parameter] public bool ShowEditorContext { get; set; }
        [Parameter] public bool ShowReturnButton { get; set; }
        [Parameter] public RenderFragment NoContentView { get; set; }

        [Inject] public IContextProvider ContextProvider { get; set; }
        [Inject] public IArticleService ArticleService { get; set; }
        [Inject] public IMatToaster Toaster { get; set; }
        [Inject] public IMatDialogService Dialog { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected override async Task OnInitializedAsync()
        {
            userContext = ContextProvider.GetUserContext();
            await SetArticles();
        }

        private async Task SetArticles()
        {
            var result = await LoadDocumentsAsync();
            serviceResult = result;
            if (result.WasSuccessful)
            {
                articles = result.Value.Select(rm => new SearchableArticle(rm));
            }
        }
        
        private void Edit(ArticleReadModel readModel)
        {
            Nav.NavigateTo("news/edit/" + readModel.Id);
        }

        private void View(ArticleReadModel readModel)
        {
            Nav.NavigateTo("news/view/" + readModel.Id);
        }

        private async Task Delete(ArticleReadModel readModel)
        {
            if (!await Dialog.ConfirmAsync($"{readModel.CurrentRevision.Title} löschen?")) return;

            var result = await ArticleService.DeleteArticleAsync(new ArticleId(readModel.Id));
            if (result.WasSuccessful)
            {
                await SetArticles();
                Toaster.Add("Artikel gelöscht", MatToastType.Info, "Erfolgreich");
            }
            else
            {
                Toaster.Add(result.ErrorMessage, MatToastType.Danger, "Error");
            }

            StateHasChanged();
        }

        private async Task ChangeVisibility(ArticleReadModel readModel)
        {
            if (!await Dialog.ConfirmAsync(readModel.IsVisible
                ? "Artikel auf unsichtbar setzen?"
                : "Artikel auf sichtbar setzen?"))
                return;

            var result = await ArticleService.ChangeVisibilityAsync(new ArticleId(readModel.Id),
                !readModel.IsVisible);
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