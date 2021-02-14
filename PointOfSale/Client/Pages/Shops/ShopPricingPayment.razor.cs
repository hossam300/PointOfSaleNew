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
    public partial class ShopPricingPayment : ComponentBase
    {
        ShopPricingPaymentDTO shop = new ShopPricingPaymentDTO();
        List<DropDownList> PaymentMethods = new List<DropDownList>();
       
        IEnumerable<int> multipleValues = new int[] { };
        [Parameter] public int? id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            await Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        async Task Load()
        {
            PaymentMethods = await Http.GetFromJsonAsync<List<DropDownList>>("/api/PaymentMethods/GetDropDownListAll");
            if (id != null)
            {
                shop = await Http.GetFromJsonAsync<ShopPricingPaymentDTO>("/api/Shops/GetShopPricingPaymentDTOById/" + id);
                multipleValues = shop.PaymentMethods.Select(x => x.PaymentMethodId);
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
        public async void FormShopSubmit(ShopPricingPaymentDTO model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));
            shop.PaymentMethods = new List<ShopPaymentMethodDTO>();
            foreach (var paymentMethod in multipleValues)
            {
                shop.PaymentMethods.Add(new ShopPaymentMethodDTO { PaymentMethodId = paymentMethod,ShopId=(int)id});
            }
           
            var shopId = 0;
            using (var response = await Http.PutAsJsonAsync<ShopPricingPaymentDTO>("/api/Shops/UpdateShopPricingPaymentDTO", shop))
            {
                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<Shop>();
                // get id property from JSON response data
                shopId = data.Id;
               
            }
            uriHelper.NavigateTo("/ShopBillsReceipts/" + shopId);
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }

}
