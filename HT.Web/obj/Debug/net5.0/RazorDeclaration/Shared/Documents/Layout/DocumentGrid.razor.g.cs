// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace HT.Web.Shared.Documents.Layout
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Web.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Web.Shared.Articles;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Blazorade.Teams;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Blazorade.Teams.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Blazorade.Teams.Configuration;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Blazorade.Teams.Interop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Blazorade.Teams.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using MatBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Application.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Application.Documents.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Microsoft.Graph;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Web.Shared.Documents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Web.Shared.Documents.Layout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Application.Documents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using HT.Application.Documents.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "c:\Users\Lycos\Desktop\HT\src\HT.Web\_Imports.razor"
using Blazored.TextEditor;

#line default
#line hidden
#nullable disable
    public partial class DocumentGrid : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 22 "c:\Users\Lycos\Desktop\HT\src\HT.Web\Shared\Documents\Layout\DocumentGrid.razor"
       

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


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
