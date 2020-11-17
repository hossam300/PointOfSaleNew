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
    public partial class ProductSales : ComponentBase
    {
        Product product = new Product();
        [Parameter] public int id { get; set; }
        IEnumerable<int> multipleValues = new int[] { };
        IEnumerable<Product> products;
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            product = await Http.GetFromJsonAsync<Product>("api/Products/GetById/" + id);
            products = await Http.GetFromJsonAsync<List<Product>>("api/Products/GetAll/");
            multipleValues = product.OptionalProducts.Select(x => x.OptionalProductId);
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        public async void FormProductSubmit(Product model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            product.OptionalProducts = new List<OptionalProduct>();
            foreach (var item in multipleValues)
            {
                product.OptionalProducts.Add(new OptionalProduct { OptionalProductId = item });
            }
          
            foreach (var item in product.CustomerTaxes)
            {
                item.Tax = null;
            }
            foreach (var item in product.VendorTaxes)
            {
                item.Tax = null;
            }
            product.Company = null;
            
            using (var response = await Http.PutAsJsonAsync<Product>("/api/Products/Update", product))
            {

                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<Product>();

                // get id property from JSON response data
                //  var customerId = data[0].Id;
                uriHelper.NavigateTo("/ProductPOS/" + data.Id);
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
