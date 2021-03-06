// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace HT.Web.Shared.Documents
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
#nullable restore
#line 1 "c:\Users\Lycos\Desktop\HT\src\HT.Web\Shared\Documents\DocumentSubscriptionButton.razor"
using HT.Application.Userdata.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "c:\Users\Lycos\Desktop\HT\src\HT.Web\Shared\Documents\DocumentSubscriptionButton.razor"
using HT.Core.Aggregates.Document;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "c:\Users\Lycos\Desktop\HT\src\HT.Web\Shared\Documents\DocumentSubscriptionButton.razor"
using HT.Core.Aggregates.Userdata;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "c:\Users\Lycos\Desktop\HT\src\HT.Web\Shared\Documents\DocumentSubscriptionButton.razor"
using System.Threading;

#line default
#line hidden
#nullable disable
    public partial class DocumentSubscriptionButton : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 22 "c:\Users\Lycos\Desktop\HT\src\HT.Web\Shared\Documents\DocumentSubscriptionButton.razor"
       

    [Parameter]
    public string DocumentId { get; set; }

    [Parameter]
    public bool AsIcon { get; set; }

    [Parameter]
    public string DocumentTitle { get; set; }

    private async Task ToggleSubscription(bool subscribe)
    {
        if(!await dialog.ConfirmAsync(subscribe ? $"Das Dokument {DocumentTitle} abonnieren?" : $"Das Dokument {DocumentTitle} deabonnieren?"))
            return;

        var context = contextProvider.GetUserContext();
        var result = subscribe
            ? await userdataService.SubscribeToDocumentAsync(new UserdataId(context.Userdata.Id),
                new DocumentId(DocumentId), CancellationToken.None)
            : await userdataService.UnsubscribeToDocumentAsync(new UserdataId(context.Userdata.Id),
                new DocumentId(DocumentId),
                CancellationToken.None);
        
        if (result.WasSuccessful)
        {
            if (subscribe)
                context.Userdata.SubscribedDocuments.Add(DocumentId);
            else
                context.Userdata.SubscribedDocuments.Remove(DocumentId);
            toaster.Add(subscribe ? "Dokument abonniert" : "Dokument deabonniert", MatToastType.Success);
        }
        else
            toaster.Add(result.ErrorMessage, MatToastType.Warning, "Error");

        StateHasChanged();
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMatDialogService dialog { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMatToaster toaster { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IUserdataService userdataService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IContextProvider contextProvider { get; set; }
    }
}
#pragma warning restore 1591
