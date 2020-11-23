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

namespace PointOfSale.Client.Pages.Orders
{
    public partial class Orders : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Parameter] public int? id { get; set; }

        protected RadzenContent content1;

        protected RadzenButton btnOrders;

        protected RadzenGrid<Order> gridOrders;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Order> _Orders;
        protected IEnumerable<Order> orders
        {
            get
            {
                return _Orders;
            }
            set
            {
                if (_Orders != value)
                {
                    _Orders = value;
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
            await gridOrders.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            if (id != null)
            {
                orders = await Http.GetFromJsonAsync<List<Order>>("/api/Orders/GetAllWithIncludeByShopId");
            }
            else
            {
                var sahlErpGetOrdersResult = await Http.GetFromJsonAsync<List<Order>>("/api/Orders/GetAllWithInclude");

                orders = sahlErpGetOrdersResult;
            }
        }
        async void PrintOrder(string id)
        {
            var order = await Http.GetFromJsonAsync<Order>("/api/Orders/GetOrderByNo/" + id);
            var result = await DialogService.OpenAsync<Sessions.PrintOrder>("Print Order", new Dictionary<string, object>() { { "Order", order } },
                    new Radzen.DialogOptions() { Width = "400px", Height = "500px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }
        async void PrintBill(string id)
        {
            var order = await Http.GetFromJsonAsync<Order>("/api/Orders/GetOrderByNo/" + id);
            var result = await DialogService.OpenAsync<Sessions.PrintBill>("Print Bill", new Dictionary<string, object>() { { "Order", order } },
                 new Radzen.DialogOptions() { Width = "400px", Height = "500px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }
        //protected async void btnOrdersClick(MouseEventArgs args)
        //{
        //    var result = await DialogService.OpenAsync<AddOrder>("Add Address Type", null);
        //    await gridOrders.Reload();

        //    await InvokeAsync(() => { StateHasChanged(); });
        //}

        //protected async void EditRow(Order args)
        //{
        //    DialogService.Open<AddOrder>("Edit Address Type", new Dictionary<string, object>() { { "Id", args.Id } },
        //              new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
        //    await InvokeAsync(() => { StateHasChanged(); });
        //}

        //protected async void GridDeleteButtonClick(MouseEventArgs args, Order data)
        //{
        //    try
        //    {
        //        var sahlErpGetOrdersResult = await Http.DeleteAsync("/api/Orders/Delete/" + data.Id);
        //        Load();
        //        if (sahlErpGetOrdersResult != null)
        //        {
        //            await gridOrders.Reload();
        //        }
        //    }
        //    catch (Exception sahlErpDeleteOrderException)
        //    {
        //        NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Order");
        //    }
        //}
    }
}
