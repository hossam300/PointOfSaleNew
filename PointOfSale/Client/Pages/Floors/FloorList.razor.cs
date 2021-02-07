using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Floors
{
    public partial class FloorList : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }



        protected RadzenContent content1;

        protected RadzenButton btnFloors;

        protected RadzenGrid<Floor> gridFloors;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Floor> _Floors;
        protected IEnumerable<Floor> Floors
        {
            get
            {
                return _Floors;
            }
            set
            {
                if (_Floors != value)
                {
                    _Floors = value;
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
            await gridFloors.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            var sahlErpGetFloorsResult = await Http.GetFromJsonAsync<List<Floor>>("/api/Floors/GetAll");

            Floors = sahlErpGetFloorsResult;
        }

        protected async void btnFloorsClick(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddFloor>("Add Address Type", null);
            await gridFloors.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void EditRow(Floor args)
        {
            DialogService.Open<AddFloor>("Edit Address Type", new Dictionary<string, object>() { { "Id", args.Id } },
                      new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, Floor data)
        {
            try
            {
                var sahlErpGetFloorsResult = await Http.DeleteAsync("/api/Floors/Delete/" + data.Id);
                Load();
                if (sahlErpGetFloorsResult != null)
                {
                    await gridFloors.Reload();
                }
            }
            catch (Exception sahlErpDeleteFloorException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Floor");
            }
        }
    }
}
