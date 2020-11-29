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

namespace PointOfSale.Client.Pages.BarcodeScanners
{
    public partial class BarcodeScanners : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }



        protected RadzenContent content1;

        protected RadzenButton btnBarcodeScanners;

        protected RadzenGrid<BarcodeScanner> gridBarcodeScanners;

        protected RadzenButton gridDeleteButton;

        IEnumerable<BarcodeScanner> _BarcodeScanners;
        protected IEnumerable<BarcodeScanner> barcodeScanners
        {
            get
            {
                return _BarcodeScanners;
            }
            set
            {
                if (_BarcodeScanners != value)
                {
                    _BarcodeScanners = value;
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
            await gridBarcodeScanners.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            var sahlErpGetBarcodeScannersResult = await Http.GetFromJsonAsync<List<BarcodeScanner>>("/api/BarcodeScanners/GetAll");

            barcodeScanners = sahlErpGetBarcodeScannersResult;
        }

        protected async void btnBarcodeScannersClick(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddBarcodeScanner>("Add Barcode Scanner", null);
            await gridBarcodeScanners.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void EditRow(BarcodeScanner args)
        {
            DialogService.Open<AddBarcodeScanner>("Edit Barcode Scanner", new Dictionary<string, object>() { { "Id", args.Id } },
                      new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, BarcodeScanner data)
        {
            try
            {
                var sahlErpGetBarcodeScannersResult = await Http.DeleteAsync("/api/BarcodeScanners/Delete/" + data.Id);
                Load();
                if (sahlErpGetBarcodeScannersResult != null)
                {
                    await gridBarcodeScanners.Reload();
                }
            }
            catch (Exception sahlErpDeleteBarcodeScannerException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete BarcodeScanner");
            }
        }
    }
}
