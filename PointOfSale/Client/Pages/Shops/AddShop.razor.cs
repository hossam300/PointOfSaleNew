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
    public partial class AddShop : ComponentBase
    {
        Shop shop = new Shop();
        List<SahlUserIdentity> AllowedEmployees = new List<SahlUserIdentity>();
        IEnumerable<string> multipleValues = new string[] { };
        [Parameter] public int? id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            await Load();
            await JSRuntime.InvokeVoidAsync("loadStyle", "/assets/css/pages/wizard/wizard-1.css");
            await JSRuntime.InvokeVoidAsync("loadScript", "/assets/js/pages/custom/projects/add-project.js");
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        async Task Load()
        {
            AllowedEmployees = await Http.GetFromJsonAsync<List<SahlUserIdentity>>("/api/Users/GetAll");
            if (id != null)
            {
                shop = await Http.GetFromJsonAsync<Shop>("/api/Shops/GetById/" + id);
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
        public async void FormShopSubmit(Shop model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));
            //bool formIsValid = model.Validate();
            if (shop.Id == 0)
            {
                foreach (var item in multipleValues)
                {
                    shop.AllowedEmployees.Add(new ShopEmployee { UserId = item });
                }

                using (var response = await Http.PostAsJsonAsync<Shop>("/api/Shops/Insert", shop))
                {
                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Shop>();

                    // get id property from JSON response data
                    //  var customerId = data.Id;
                    uriHelper.NavigateTo("/ShopInterface/" + data.Id);
                }

            }
            else
            {
                shop.AllowedEmployees = new List<ShopEmployee>();
                //product.Company = null;
                foreach (var item in multipleValues)
                {
                    shop.AllowedEmployees.Add(new ShopEmployee { UserId = item });
                }
                using (var response = await Http.PutAsJsonAsync<Shop>("/api/Shops/Update", shop))
                {

                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Shop>();

                    // get id property from JSON response data
                    //  var customerId = data[0].Id;
                    uriHelper.NavigateTo("/ShopInterface/" + data.Id);
                }
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
