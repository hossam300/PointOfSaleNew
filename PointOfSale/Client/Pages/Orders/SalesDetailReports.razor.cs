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

namespace PointOfSale.Client.Pages.Orders
{
    public partial class SalesDetailReports : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        public int? id { get; set; }
        DateTime StartDate;
        DateTime EndDate;
        protected RadzenGrid<Order> gridOrders;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Order> _Orders;
        List<PaymentMethodDTO> OrderPayments;
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
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = DateTime.Now;
            _Orders = new List<Order>();
            OrderPayments = new List<PaymentMethodDTO>();
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        void OnChange(DateTime? value, string name, string format)
        {
            //  console.Log($"{name} value changed to {value?.ToString(format)}");
        }
        async void Search()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            orders = await Http.GetFromJsonAsync<List<Order>>("/api/Orders/GetAllOrderOnPeriod/?StartDate=" + StartDate + "&&EndDate=" + EndDate);
            OrderPayments = await Http.GetFromJsonAsync<List<PaymentMethodDTO>>("/api/OrderPayments/GetAllGroubByMethodOnPeriod/?StartDate=" + StartDate + "&&EndDate=" + EndDate);
            gridOrders.Reload();
            InvokeAsync(() => { StateHasChanged(); });
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
