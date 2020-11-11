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
    public partial class  Products : ComponentBase
    {
        private string searchTerm { get; set; } = "";
        List<Product> Model => ProductList.Where(i => i.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        List<Product> ProductList = new List<Product>();
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            ProductList = await Http.GetFromJsonAsync<List<Product>>("/api/Products/GetAll");
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        async void DisActive(int productId)
        {
            await Http.GetFromJsonAsync<bool>("/api/Products/DisActive/" + productId);
        }
    }
}
