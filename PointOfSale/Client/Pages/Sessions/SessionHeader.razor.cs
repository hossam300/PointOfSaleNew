using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Sessions
{
    public partial class SessionHeader : ComponentBase
    {
        [Inject] SignOutSessionStateManager SignOutManager { get; set; }
        [Inject] DialogService DialogService { get; set; }

        string Date = DateTime.Now.ToString("f");
        protected override async Task OnInitializedAsync()
        {
            DialogService.OnOpen += Open;
            DialogService.OnClose += Close;
            new Timer(new TimerCallback(_ =>
            {
                Date = DateTime.Now.ToString("f");

                // Note that the following line is necessary because otherwise
                // Blazor would not recognize the state change and not refresh the UI
                this.StateHasChanged();
            }), null, 60000, 60000);
        }
        
        void StartCountdown()
        {
          
        }
        public async void Closeregiste()
        {
            var result = await DialogService.OpenAsync<CloseRegister>("Close Registe", null);
            await InvokeAsync(() => { StateHasChanged(); });
        }
        public void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
            //events.Add(DateTime.Now, "Dialog opened");
            StateHasChanged();
        }

        public async void Close(dynamic result)
        {
            //events.Add(DateTime.Now, "Dialog closed. Result: " + result);

            StateHasChanged();
        }
        public async void OpenTodaySale()
        {
            var result = await DialogService.OpenAsync<TodaySale>("Today's Sale", null);
            await InvokeAsync(() => { StateHasChanged(); });
        }
        public async void OpenRegisterDetails()
        {
            var result = await DialogService.OpenAsync<RegisterDetail>("Register Details", null);
            await InvokeAsync(() => { StateHasChanged(); });
        }
    }
}
