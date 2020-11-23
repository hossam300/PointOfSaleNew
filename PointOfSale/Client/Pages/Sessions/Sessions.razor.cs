using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Sessions
{
    public partial class Sessions : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }



        protected RadzenContent content1;

        protected RadzenButton btnSessions;

        protected RadzenGrid<Session> gridSessions;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Session> _Sessions;
        protected IEnumerable<Session> sessions
        {
            get
            {
                return _Sessions;
            }
            set
            {
                if (_Sessions != value)
                {
                    _Sessions = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Load();
            DialogService.OnOpen += Open;
            DialogService.OnClose += Close;
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
            //events.Add(DateTime.Now, "Dialog opened");
            StateHasChanged();
        }

        async void Close(dynamic result)
        {
            //events.Add(DateTime.Now, "Dialog closed. Result: " + result);
            Load();
            await gridSessions.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            var sahlErpGetSessionsResult = await Http.GetFromJsonAsync<List<Session>>("/api/Sessions/GetAll");

            sessions = sahlErpGetSessionsResult;
        }

        //protected async void btnSessionsClick(MouseEventArgs args)
        //{
        //    var result = await DialogService.OpenAsync<AddSession>("Add Address Type", null);
        //    await gridSessions.Reload();

        //    await InvokeAsync(() => { StateHasChanged(); });
        //}

        //protected async void EditRow(Session args)
        //{
        //    DialogService.Open<AddSession>("Edit Address Type", new Dictionary<string, object>() { { "Id", args.Id } },
        //              new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
        //    await InvokeAsync(() => { StateHasChanged(); });
        //}

        //protected async void GridDeleteButtonClick(MouseEventArgs args, Session data)
        //{
        //    try
        //    {
        //        var sahlErpGetSessionsResult = await Http.DeleteAsync("/api/Sessions/Delete/" + data.Id);
        //        Load();
        //        if (sahlErpGetSessionsResult != null)
        //        {
        //            await gridSessions.Reload();
        //        }
        //    }
        //    catch (Exception sahlErpDeleteSessionException)
        //    {
        //        NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Session");
        //    }
        //}
    }
}
