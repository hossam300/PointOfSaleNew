using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.BarcodeScanners
{
    public partial class AddBarcodeScanner : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }
       
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

       
        [Parameter] public int? Id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<BarcodeScanner> formBarcodeScanner;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        BarcodeScanner _BarcodeScanner;
        protected BarcodeScanner BarcodeScanner
        {
            get
            {
                return _BarcodeScanner;
            }
            set
            {
                if (_BarcodeScanner != value)
                {
                    _BarcodeScanner = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }

        protected async void Load()
        {
            if (Id != null)
            {
                BarcodeScanner = await Http.GetFromJsonAsync<BarcodeScanner>("/api/BarcodeScanners/GetById/" + Id);
            }
            else BarcodeScanner = new BarcodeScanner();
        }

        protected async void FormBarcodeScannerSubmit(BarcodeScanner args)
        {
            try
            {
                if (BarcodeScanner.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<BarcodeScanner>("/api/BarcodeScanners/Update", BarcodeScanner))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<BarcodeScanner>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/BarcodeScanners");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<BarcodeScanner>("/api/BarcodeScanners/Insert", BarcodeScanner))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<BarcodeScanner>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/BarcodeScanners");
                    }
                }
                DialogService.Close(BarcodeScanner);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new BarcodeScanner!");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
