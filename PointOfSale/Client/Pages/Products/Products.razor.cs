using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Products
{
    public partial class Products : ComponentBase
    {
        private string searchTerm { get; set; } = "";
        [Inject]
        protected DialogService DialogService { get; set; }
        List<Product> Model => ProductList.Where(i => (searchTerm == "") ? i.Name != null : i.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        List<Product> ProductList = new List<Product>();
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            ProductList = await Http.GetFromJsonAsync<List<Product>>("/api/Products/GetAll");
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

            StateHasChanged();
        }
        async void DisActive(int productId)
        {
            await Http.GetFromJsonAsync<bool>("/api/Products/DisActive/" + productId);
        }
        async void PrintBarcode(string barcode)
        {
            var result = await DialogService.OpenAsync<PrintBarcode>(Loc["PrintBarcode"], new Dictionary<string, object>() { { "Id", barcode } });

            await InvokeAsync(() => { StateHasChanged(); });
        }
    }
}
