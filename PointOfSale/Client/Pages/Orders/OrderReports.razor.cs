using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using Radzen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Orders
{
    public partial class OrderReports : ComponentBase
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

        List<PiChartsDTO> OrderPayments = new List<PiChartsDTO>();
        List<PiChartsDTO> Categories = new List<PiChartsDTO>();
        List<PiChartsDTO> Customers = new List<PiChartsDTO>();

      
        string FormatAsUSD(object value)
        {
            return ((double)value).ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
        }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = DateTime.Now;
            OrderPayments = await Http.GetFromJsonAsync<List<PiChartsDTO>>("/api/OrderPayments/GetChartGroubByMethodOnPeriod/?StartDate=" + StartDate + "&&EndDate=" + EndDate);
            Categories = await Http.GetFromJsonAsync<List<PiChartsDTO>>("/api/OrderItems/GetChartGroubByProductCategoryOnPeriod/?StartDate=" + StartDate + "&&EndDate=" + EndDate);
            Customers = await Http.GetFromJsonAsync<List<PiChartsDTO>>("/api/Customers/GetChartGroubByDate/?StartDate=" + StartDate + "&&EndDate=" + EndDate);
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        void OnChange(DateTime? value, string name, string format)
        {
            //  console.Log($"{name} value changed to {value?.ToString(format)}");
        }
        async void Search()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            OrderPayments = await Http.GetFromJsonAsync<List<PiChartsDTO>>("/api/OrderPayments/GetChartGroubByMethodOnPeriod/?StartDate=" + StartDate + "&&EndDate=" + EndDate);
            Categories = await Http.GetFromJsonAsync<List<PiChartsDTO>>("/api/OrderItems/GetChartGroubByProductCategoryOnPeriod/?StartDate=" + StartDate + "&&EndDate=" + EndDate);
            Customers = await Http.GetFromJsonAsync<List<PiChartsDTO>>("/api/Customers/GetChartGroubByDate/?StartDate=" + StartDate + "&&EndDate=" + EndDate);
            InvokeAsync(() => { StateHasChanged(); });
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
