using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Sessions
{
    public partial class Create : ComponentBase
    {
        [Parameter]
        public int id { get; set; }
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        //[Inject]
        //UserManager<SahlApplication> UserManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            await JSRuntime.InvokeVoidAsync("removeScript");
            await JSRuntime.InvokeVoidAsync("loadScript", "/js/jQuery-2.1.4.min.js");
            await JSRuntime.InvokeVoidAsync("loadScript", "/themes/default/assets/dist/js/libraries.min.js");
          
           
            await JSRuntime.InvokeVoidAsync("FullScreen");
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
       
    }

}
