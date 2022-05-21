using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HT.Application.Documents;
using HT.Application.Documents.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Shared.Documents.Layout
{
    partial class DocumentTable
    {
        private MatSortChangedEvent lastSort;
        private SearchableDocument selectedDocument;
        private ISearchableContent<DocumentReadModel>[] sortedDocuments => SortData();

        [Inject] public IMatToaster Toaster { get; set; }
        [Inject] public IMatDialogService Dialog { get; set; }
        [Inject] public IDocumentService DocumentService { get; set; }

        [Parameter]
        public IEnumerable<ISearchableContent<DocumentReadModel>> Documents { get; set; }

        [Parameter]
        public EventCallback<IEnumerable<ISearchableContent<DocumentReadModel>>> DocumentsChanged { get; set; }

        [Parameter] public bool ShowEditContext { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Restore { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Delete { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> ChangeVisibility { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Edit { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> View { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetSortArg(null);
        }

        private void OnSelect(object obj)
        {
            selectedDocument = null;
            StateHasChanged();
            selectedDocument = obj as SearchableDocument;
            StateHasChanged();
        }


        private async Task DeleteAndSort()
        {
            if (!Delete.HasDelegate)
                return;
            await Delete.InvokeAsync(selectedDocument.GetContent());
            SetSortArg(lastSort);
        }

        private async Task RestoreAndSort()
        {
            if (!Restore.HasDelegate)
                return;
            await Restore.InvokeAsync(selectedDocument.GetContent());
            SetSortArg(lastSort);
        }

        private void SetSortArg(MatSortChangedEvent args) => lastSort = args;

        private SearchableDocument[] SortData()
        {
            var docs = Documents.Select(d => (SearchableDocument)d ).ToArray();
            if (lastSort == null ||  string.IsNullOrEmpty(lastSort.SortId))
                return docs;

            Comparison<SearchableDocument> comparison = lastSort.SortId switch
            {
                "title" => (s1, s2) => string.Compare(s1.GetContent().Revisions.Last().Title,
                    s2.GetContent().Revisions.Last().Title, StringComparison.InvariantCultureIgnoreCase),
                "category" => (s1, s2) => string.Compare(s1.GetContent().Category, s2.GetContent().Category,
                    StringComparison.InvariantCultureIgnoreCase),
                "date" => (s1, s2) =>
                    s1.GetContent().Revisions.Last().Date.CompareTo(s2.GetContent().Revisions.Last().Date),
                "editor" => (s1, s2) => string.Compare(s1.GetContent().Revisions.Last().UserContext.DisplayName,
                    s2.GetContent().Revisions.Last().UserContext.DisplayName,
                    StringComparison.InvariantCultureIgnoreCase),
                "state" => (s1, s2) =>
                    s1.GetContent().MarkedForDeletion
                        ? s1.GetContent().DeletionTime.CompareTo(s2.GetContent().DeletionTime)
                        : s1.GetContent().IsVisible.CompareTo(s2.GetContent().IsVisible),
                _ => null
            };

            if (comparison == null) return docs;

            if (lastSort.Direction == MatSortDirection.Desc)
            {
                Array.Sort(docs, (s1, s2) => -1 * comparison(s1, s2));
            }
            else
            {
                Array.Sort(docs, comparison);
            }

            return docs;
        }
    }
}