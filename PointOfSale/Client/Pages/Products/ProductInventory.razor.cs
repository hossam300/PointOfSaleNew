using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Products
{
    public partial class ProductInventory : ComponentBase
    {
        ProductInventoryDTO product = new ProductInventoryDTO();
        [Parameter] public int id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            product = await Http.GetFromJsonAsync<ProductInventoryDTO>("api/Products/GetProductInventoryDTOById/" + id);
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        public async void FormProductSubmit(ProductInventoryDTO model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            //bool formIsValid = model.Validate();
            //foreach (var item in product.OptionalProducts)
            //{
            //    item.Product.Company = null;
            //}
            using (var response = await Http.PostAsJsonAsync<ProductInventoryDTO>("/api/Products/UpdateProductInventoryDTO", product))
            {
                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<ProductInventoryDTO>();

                // get id property from JSON response data
                //  var customerId = data[0].Id;
                uriHelper.NavigateTo("/Products");
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
