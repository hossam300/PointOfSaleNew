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
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            var sessions = await Http.GetFromJsonAsync<Session>("/api/Sessions/GetByShopId/" + id);
            if (sessions != null)
            {
                Session session = new Session()
                {
                    SessionNo = RandomString(5),
                    CreationDate = DateTime.Now,
                    ShopId = id,
                    Status = Status.Open,
                    CreatorId = users
                };
                await Http.PostAsJsonAsync<Session>("/api/Sessions/Insert", session);
            }
            await JSRuntime.InvokeVoidAsync("FullScreen");
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        private Random random = new Random((int)DateTime.Now.Ticks);
        private string RandomString(int length)
        {
            const string pool = "0123456789";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }
    }

}
