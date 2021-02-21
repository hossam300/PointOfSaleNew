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
        ProductSalesDTO product = new ProductSalesDTO();
        [Parameter] public int id { get; set; }
        IEnumerable<int> multipleValues = new int[] { };
        IEnumerable<DropDownList> products;
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            product = await Http.GetFromJsonAsync<ProductSalesDTO>("api/Products/GetProductSalesDTOById/" + id);
            products = await Http.GetFromJsonAsync<List<DropDownList>>("api/Products/GetDropDownListAll/");
            products = products.Where(c => c.Id != id).ToList();
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
        public async void FormProductSubmit(ProductSalesDTO model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            product.OptionalProducts = new List<OptionalProductDTO>();
            foreach (var item in multipleValues)
            {
                product.OptionalProducts.Add(new OptionalProductDTO { OptionalProductId = item });
            }
            int productId = 0;
            using (var response = await Http.PostAsJsonAsync<ProductSalesDTO>("/api/Products/UpdateProductSalesDTO", product))
            {

                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<ProductSalesDTO>();

                // get id property from JSON response data
                //  var productId = data[0].Id;
              
            }
            uriHelper.NavigateTo("/ProductPOS/" + id);
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
