using System.Collections.Generic;
using System.Linq;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace HT.Web.Shared
{
    partial class SearchableContentView<T>
    {
        private BaseMatMenu menu;
        private MatIconButton menuButton;
        private string queryString;

        private IEnumerable<ISearchableContent<T>> filteredContent
        {
            get
            {
                var result = string.IsNullOrEmpty(queryString) ? Content : FilterContent(Content, queryString);
                return result;
            }
        }

        [Parameter] public IEnumerable<ISearchableContent<T>> Content { get; set; }
        [Parameter] public RenderFragment<IEnumerable<ISearchableContent<T>>> ContentLayout { get; set; }
        [Parameter] public RenderFragment NoContentView { get; set; }
        [Parameter] public RenderFragment NavList { get; set; }
        [Parameter] public RenderFragment Actions { get; set; }
        [Parameter] public string AppbarTitle { get; set; }
        [Parameter] public string SearchPlaceHolder { get; set; }
        [Parameter] public bool ShowMenu { get; set; } = true;

        private void OpenMenu()
        {
            menu.OpenAsync(menuButton.Ref);
        }

        private static IEnumerable<ISearchableContent<T>> FilterContent(IEnumerable<ISearchableContent<T>> content,
            string query)
        {
            foreach (var c in content)
            {
                if (c.GetSearchableKeywords().Any(k => k.ToLower().Contains(query.ToLower())))
                    yield return c;
            }
        }
    }
}