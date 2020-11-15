using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
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
        List<SahlUserIdentity> AllowedEmployees = new List<SahlUserIdentity>();
        List<Floor> Floors = new List<Floor>();
        List<ShopPrinter> Printers = new List<ShopPrinter>();
        List<FiscalPointOfSaleition> FiscalPointOfSaleitions = new List<FiscalPointOfSaleition>();
        List<ProductCategory> AvailableCategories = new List<ProductCategory>();
        IEnumerable<int> multipleFloors = new int[] { };
        IEnumerable<int> multiplePrinters = new int[] { };
        IEnumerable<int> multipleAvailableCategories = new int[] { };
        [Parameter] public int? id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Load();
            await JSRuntime.InvokeVoidAsync("loadStyle", "/assets/css/pages/wizard/wizard-1.css");
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        async void Load()
        {
            Floors = await Http.GetFromJsonAsync<List<Floor>>("/api/Floors/GetAll");
            Printers = await Http.GetFromJsonAsync<List<ShopPrinter>>("/api/Printers/GetAll");
            FiscalPointOfSaleitions = await Http.GetFromJsonAsync<List<FiscalPointOfSaleition>>("/api/FiscalPointOfSaleitions/GetAll");
            AvailableCategories = await Http.GetFromJsonAsync<List<ProductCategory>>("/api/ProductCategories/GetAll");
            if (id != null)
            {
                shop = await Http.GetFromJsonAsync<Shop>("/api/Shops/GetById/" + id);
                multipleFloors = shop.Floors.Select(x => x.Id);
                multiplePrinters = shop.Printers.Select(x => x.Id);
                multipleAvailableCategories = shop.AvailableCategories.Select(x => x.Id);
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
            foreach (var item in multipleFloors)
            {
                shop.Floors.Add(new ShopFloor { Id = item });
            }
            foreach (var item in multiplePrinters)
            {
                shop.Printers.Add(new ShopPrinter { Id = item });
            }
            foreach (var item in multipleAvailableCategories)
            {
                shop.AvailableCategories.Add(new ShopProductCategory { Id = item });
            }
            //bool formIsValid = model.Validate();
            if (shop.Id == 0)
            {


                using (var response = await Http.PostAsJsonAsync<Shop>("/api/Shops/Insert", shop))
                {
                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Shop>();

                    // get id property from JSON response data
                    //  var customerId = data.Id;
                    uriHelper.NavigateTo("/ShopPricingPayment/" + data.Id);
                }

            }
            else
            {
                //   shop.AllowedEmployees = new List<AspNetUser>();
                //product.Company = null;

                using (var response = await Http.PutAsJsonAsync<Shop>("/api/Shops/Update", shop))
                {

                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Shop>();

                    // get id property from JSON response data
                    //  var customerId = data[0].Id;
                    uriHelper.NavigateTo("/ShopPricingPayment/" + data.Id);
                }
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }

}
