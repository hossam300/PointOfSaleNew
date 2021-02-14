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
        ShopInterfaceDTO shop = new ShopInterfaceDTO();
        List<DropDownList> Floors = new List<DropDownList>();
        List<DropDownList> Printers = new List<DropDownList>();
        List<DropDownList> FiscalPositions = new List<DropDownList>();
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
            FiscalPositions = await Http.GetFromJsonAsync<List<DropDownList>>("/api/FiscalPositions/GetDropDownListAll");
            AvailableCategories = await Http.GetFromJsonAsync<List<DropDownList>>("/api/ProductCategories/GetDropDownListAll");
            if (id != null)
            {
                shop = await Http.GetFromJsonAsync<ShopInterfaceDTO>("/api/Shops/GetShopInterfaceDTOById/" + id);
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
        public async void FormShopSubmit(ShopInterfaceDTO model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));
            shop.Floors = new List<ShopFloorDTO>();
            shop.Printers = new List<ShopPrinterDTO>();
            shop.AvailableCategories = new List<ShopProductCategoryDTO>();
            foreach (var Floor in multipleFloors)
            {
                shop.Floors.Add(new ShopFloorDTO { FloorId = Floor, ShopId = shop.Id });
            }
            foreach (var Printer in multiplePrinters)
            {
                shop.Printers.Add(new ShopPrinterDTO { PrinterId = Printer, ShopId = shop.Id });
            }
            foreach (var item in multipleAvailableCategories)
            {
                shop.AvailableCategories.Add(new ShopProductCategoryDTO { ProductCategoryId = item, ShopId = shop.Id });
            }
           
            var shopId = 0;
            using (var response = await Http.PostAsJsonAsync<ShopInterfaceDTO>("/api/Shops/UpdateShopInterfaceDTO", shop))
            {

                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<ShopInterfaceDTO>();

                // get id property from JSON response data
                shopId = data.Id;
                
            }
            uriHelper.NavigateTo("/ShopPricingPayment/" + shopId);
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }

}
