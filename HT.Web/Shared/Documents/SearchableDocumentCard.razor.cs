using HT.Application.Documents;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Shared.Documents
{
    partial class SearchableDocumentCard : ComponentBase
    {
        [Parameter] public DocumentReadModel ReadModel { get; set; }
        [Parameter] public bool ShowEditContext { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Restore { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Delete { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> ChangeVisibility { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Edit { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> View { get; set; }
    }
}