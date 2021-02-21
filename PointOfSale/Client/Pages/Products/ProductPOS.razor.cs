using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace PointOfSale.Client.Pages.Products
{
    public partial class ProductPOS : ComponentBase
    {
        ProductPOSDTO product = new ProductPOSDTO();
        [Parameter] public int id { get; set; }
        List<ProductCategoryDTO> categories = new List<ProductCategoryDTO>();
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            product = await Http.GetFromJsonAsync<ProductPOSDTO>("api/Products/GetProductPOSDTOById/" + id);
            categories = await Http.GetFromJsonAsync<List<ProductCategoryDTO>>("api/ProductCategories/GetAll");
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        public async void FormProductSubmit(ProductPOSDTO model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            //bool formIsValid = model.Validate(); 
          
            using (var response = await Http.PostAsJsonAsync<ProductPOSDTO>("/api/Products/UpdateProductPOSDTO", product))
            {
                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<ProductPOSDTO>();

                // get id property from JSON response data
                //  var customerId = data[0].Id;
                uriHelper.NavigateTo("/ProductPurchase/" + id);
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
