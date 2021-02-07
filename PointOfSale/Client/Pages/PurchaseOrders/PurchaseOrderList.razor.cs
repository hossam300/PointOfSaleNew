using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.PurchaseOrders
{
    public partial class PurchaseOrderList : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }
        //[Inject]
        //protected HttpClient Http2 { get; set; }
        //[Inject]
        //protected JSRuntime JSRuntime2 { get; set; }
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Parameter] public int? id { get; set; }
        [Parameter] public int? SupplierId { get; set; }

        public RadzenContent content1;

        public RadzenButton btnPurchaseOrders;

        public RadzenGrid<PurchaseOrder> gridPurchaseOrders;

        public RadzenButton gridDeleteButton;

        IEnumerable<PurchaseOrder> _PurchaseOrders;
        public IEnumerable<PurchaseOrder> PurchaseOrders
        {
            get
            {
                return _PurchaseOrders;
            }
            set
            {
                if (_PurchaseOrders != value)
                {
                    _PurchaseOrders = value;
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
            await gridPurchaseOrders.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            if (id != null)
            {
                PurchaseOrders = await Http.GetFromJsonAsync<List<PurchaseOrder>>("/api/PurchaseOrders/GetAllWithIncludeByShopId/" + id);
            }
            else if (SupplierId != null)
            {
                PurchaseOrders = await Http.GetFromJsonAsync<List<PurchaseOrder>>("/api/PurchaseOrders/GetAllWithIncludeByCustomerId/" + SupplierId);
            }
            else
            {
                var sahlErpGetPurchaseOrdersResult = await Http.GetFromJsonAsync<List<PurchaseOrder>>("/api/PurchaseOrders/GetAllWithInclude");

                PurchaseOrders = sahlErpGetPurchaseOrdersResult;
            }
        }
        public async void PrintPurchaseOrder(string id)
        {
            var PurchaseOrder = await Http.GetFromJsonAsync<PurchaseOrder>("/api/PurchaseOrders/GetPurchaseOrderByNo/" + id);
            var result = await DialogService.OpenAsync<PrintPurchaseOrder>("Print PurchaseOrder", new Dictionary<string, object>() { { "PurchaseOrder", PurchaseOrder } },
                    new Radzen.DialogOptions() { Width = "400px", Height = "500px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }
        public async void PrintBill(string id)
        {
            var PurchaseOrder = await Http.GetFromJsonAsync<PurchaseOrder>("/api/PurchaseOrders/GetPurchaseOrderByNo/" + id);
            var result = await DialogService.OpenAsync<PrintPurchasBill>("Print Bill", new Dictionary<string, object>() { { "PurchaseOrder", PurchaseOrder } },
                 new Radzen.DialogOptions() { Width = "400px", Height = "500px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }
        //protected async void btnPurchaseOrdersClick(MouseEventArgs args)
        //{
        //    var result = await DialogService.OpenAsync<AddPurchaseOrder>("Add Address Type", null);
        //    await gridPurchaseOrders.Reload();

        //    await InvokeAsync(() => { StateHasChanged(); });
        //}

        //protected async void EditRow(PurchaseOrder args)
        //{
        //    DialogService.Open<AddPurchaseOrder>("Edit Address Type", new Dictionary<string, object>() { { "Id", args.Id } },
        //              new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
        //    await InvokeAsync(() => { StateHasChanged(); });
        //}

        //protected async void GridDeleteButtonClick(MouseEventArgs args, PurchaseOrder data)
        //{
        //    try
        //    {
        //        var sahlErpGetPurchaseOrdersResult = await Http.DeleteAsync("/api/PurchaseOrders/Delete/" + data.Id);
        //        Load();
        //        if (sahlErpGetPurchaseOrdersResult != null)
        //        {
        //            await gridPurchaseOrders.Reload();
        //        }
        //    }
        //    catch (Exception sahlErpDeletePurchaseOrderException)
        //    {
        //        NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete PurchaseOrder");
        //    }
        //}
    }
}
