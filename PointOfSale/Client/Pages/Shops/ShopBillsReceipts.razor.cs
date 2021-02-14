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
    public partial class ShopBillsReceipts : ComponentBase
    {
        ShopBillsReceiptDTO shop = new ShopBillsReceiptDTO();


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

            if (id != null)
            {
                shop = await Http.GetFromJsonAsync<ShopBillsReceiptDTO>("/api/Shops/GetShopBillsReceiptDTOById/" + id);

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
        public async void FormShopSubmit(ShopBillsReceiptDTO model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));

           
            //bool formIsValid = model.Validate();
            using (var response = await Http.PostAsJsonAsync<ShopBillsReceiptDTO>("/api/Shops/UpdateShopBillsReceiptDTO", shop))
            {
                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<Shop>();
                // get id property from JSON response data
                //  var customerId = data[0].Id;

                
            }
            uriHelper.NavigateTo("/");
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
