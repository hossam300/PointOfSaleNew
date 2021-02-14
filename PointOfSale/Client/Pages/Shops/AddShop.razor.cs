using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Shops
{
    public partial class AddShop : ComponentBase
    {
        AddShopDTO shop = new AddShopDTO();
        List<SahlUserIdentity> AllowedEmployees = new List<SahlUserIdentity>();
        IEnumerable<string> multipleValues = new string[] { };
        protected RadzenRequiredValidator ShopNameRequird;
        [Parameter] public int? id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            await Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        async Task Load()
        {
            AllowedEmployees = await Http.GetFromJsonAsync<List<SahlUserIdentity>>("/api/Users/GetAll");
            if (id != null)
            {
                shop = await Http.GetFromJsonAsync<AddShopDTO>("/api/Shops/GetAddShopDTOById/" + id);
                multipleValues = shop.AllowedEmployees.Select(x => x.UserId);
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
        void OnInvalidSubmit(Radzen.FormInvalidSubmitEventArgs args)
        {
            Log("InvalidSubmit", JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true }));
        }
        public async void FormShopSubmit(AddShopDTO model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            bool formIsValid = (model.Name == "" || model.Name == null);
            if (formIsValid)
            {
                ShopNameRequird.Text = Loc2["ShopRequired"];
                ShopNameRequird.Visible = true;

                await JSRuntime.InvokeVoidAsync("StopLoading");
                return;
            }
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));
            if (shop.Id == 0)
            {
                shop.AllowedEmployees = new List<ShopEmployeeDTO>();
                foreach (var item in multipleValues)
                {
                    shop.AllowedEmployees.Add(new ShopEmployeeDTO { UserId = item });
                }
                var shopId = 0;
                using (var response = await Http.PostAsJsonAsync<AddShopDTO>("/api/Shops/InsertAddShop", shop))
                {
                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<AddShopDTO>();

                    // get id property from JSON response data
                    shopId = data.Id;

                }
                uriHelper.NavigateTo("/ShopInterface/" + shopId);
            }
            else
            {
                shop.AllowedEmployees = new List<ShopEmployeeDTO>();
                //product.Company = null;
                foreach (var item in multipleValues)
                {
                    shop.AllowedEmployees.Add(new ShopEmployeeDTO { UserId = item, ShopId =(int) id });
                }

                using (var response = await Http.PostAsJsonAsync<AddShopDTO>("/api/Shops/UpdateAddShop", shop))
                {

                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<AddShopDTO>();

                    // get id property from JSON response data
                    //  var customerId = data[0].Id;
                    uriHelper.NavigateTo("/ShopInterface/" + data.Id);
                }
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
