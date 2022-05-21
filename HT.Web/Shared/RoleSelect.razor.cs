using System.Collections.Generic;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Shared
{
    partial class RoleSelect
    {
        private MatButton button;
        private MatMenu menu;

        [Parameter] public List<string> Source { get; set; }
        [Parameter] public List<string> SelectedItems { get; set; }
        [Parameter] public EventCallback<List<string>> SelectedItemsChanged { get; set; }
        [Parameter] public string ButtonLabel { get; set; }

        private async Task ToggleMenu()
        {
            await menu?.OpenAsync(button.Ref);
        }

        private void AddOrRemoveItem(bool value, string item)
        {
            if (value && !SelectedItems.Contains(item))
                SelectedItems.Add(item);
            else
            {
                SelectedItems.Remove(item);
            }

            StateHasChanged();
        }
    }
}