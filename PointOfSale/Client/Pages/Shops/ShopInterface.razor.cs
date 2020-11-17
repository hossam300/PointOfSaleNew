using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Shops
{
    public partial class ShopInterface : ComponentBase
    {
        Shop shop = new Shop();
        List<DropDownList> Floors = new List<DropDownList>();
        List<DropDownList> Printers = new List<DropDownList>();
        List<DropDownList> FiscalPointOfSaleitions = new List<DropDownList>();
        List<DropDownList> AvailableCategories = new List<DropDownList>();
        IEnumerable<int> multipleFloors = new int[] { };
        IEnumerable<int> multiplePrinters = new int[] { };
        IEnumerable<int> multipleAvailableCategories = new int[] { };
        [Parameter] public int? id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            await Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        async Task Load()
        {
            Floors = await Http.GetFromJsonAsync<List<DropDownList>>("/api/Floors/GetDropDownListAll");
            Printers = await Http.GetFromJsonAsync<List<DropDownList>>("/api/Printers/GetDropDownListAll");
            FiscalPointOfSaleitions = await Http.GetFromJsonAsync<List<DropDownList>>("/api/FiscalPositions/GetDropDownListAll");
            AvailableCategories = await Http.GetFromJsonAsync<List<DropDownList>>("/api/ProductCategories/GetDropDownListAll");
            if (id != null)
            {
                shop = await Http.GetFromJsonAsync<Shop>("/api/Shops/GetById/" + id);
                multipleFloors = shop.Floors.Select(x => x.FloorId);
                multiplePrinters = shop.Printers.Select(x => x.PrinterId);
                multipleAvailableCategories = shop.AvailableCategories.Select(x => x.ProductCategoryId);
            }
        }
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        void Log(string eventName, string value)
        {
            events.Add(DateTime.Now, $"{eventName}: {value}");
        }
        public async void FormShopSubmit(Shop model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));
            shop.Floors = new List<ShopFloor>();
            shop.Printers = new List<ShopPrinter>();
            shop.AvailableCategories = new List<ShopProductCategory>();
            foreach (var Floor in multipleFloors)
            {
                shop.Floors.Add(new ShopFloor { FloorId = Floor, ShopId = shop.Id });
            }
            foreach (var Printer in multiplePrinters)
            {
                shop.Printers.Add(new ShopPrinter { PrinterId = Printer, ShopId = shop.Id });
            }
            foreach (var item in multipleAvailableCategories)
            {
                shop.AvailableCategories.Add(new ShopProductCategory { ProductCategoryId = item, ShopId = shop.Id });
            }
            foreach (var item in shop.AllowedEmployees)
            {
                item.User = null;
            }
            shop.FiscalPointOfSaleition = null;
            using (var response = await Http.PutAsJsonAsync<Shop>("/api/Shops/Update", shop))
            {

                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<Shop>();

                // get id property from JSON response data
                //  var customerId = data[0].Id;
                uriHelper.NavigateTo("/ShopPricingPayment/" + data.Id);
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }

}
