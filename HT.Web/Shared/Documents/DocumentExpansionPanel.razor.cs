using System.Collections.Generic;
using HT.Application.Documents;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Shared.Documents
{
    partial class DocumentExpansionPanel
    {
        [Parameter] public string Category { get; set; }
        [Parameter] public IEnumerable<ISearchableContent<DocumentReadModel>> Documents { get; set; }
        [Parameter] public bool ShowEditContext { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Restore { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Delete { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> ChangeVisibility { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> Edit { get; set; }
        [Parameter] public EventCallback<DocumentReadModel> View { get; set; }
    }
}