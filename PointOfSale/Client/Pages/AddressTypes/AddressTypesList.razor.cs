using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.AddressTypes
{
    public partial class AddressTypesList : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }



        protected RadzenContent content1;

        protected RadzenButton btnAddressTypes;

        protected RadzenGrid<AddressType> gridAddressTypes;

        protected RadzenButton gridDeleteButton;

        IEnumerable<AddressType> _AddressTypes;
        protected IEnumerable<AddressType> AddressTypes
        {
            get
            {
                return _AddressTypes;
            }
            set
            {
                if (_AddressTypes != value)
                {
                    _AddressTypes = value;
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
            await gridAddressTypes.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            var sahlErpGetAddressTypesResult = await Http.GetFromJsonAsync<List<AddressType>>("/api/AddressTypes/GetAll");

            AddressTypes = sahlErpGetAddressTypesResult;
        }

        protected async void btnAddressTypesClick(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddAddressType>("Add Address Type", null);
            await gridAddressTypes.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void EditRow(AddressType args)
        {
            DialogService.Open<AddAddressType>("Edit Address Type", new Dictionary<string, object>() { { "Id", args.Id } },
                      new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, AddressType data)
        {
            try
            {
                var sahlErpGetAddressTypesResult = await Http.DeleteAsync("/api/AddressTypes/Delete/" + data.Id);
                Load();
                if (sahlErpGetAddressTypesResult != null)
                {
                    await gridAddressTypes.Reload();
                }
            }
            catch (Exception sahlErpDeleteAddressTypeException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete AddressType");
            }
        }
    }
}
