using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Suppliers
{
    public partial class SuppliersList : ComponentBase
    {
        private string searchTerm { get; set; } = "";
        List<Supplier> Model => Suppliers.Where(i => i.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        List<Supplier> Suppliers = new List<Supplier>();
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Suppliers = await Http.GetFromJsonAsync<List<Supplier>>("/api/Suppliers/GetAll");
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        async void DisActive(int SupplierId)
        {
            await Http.GetFromJsonAsync<bool>("/api/Suppliers/DisActive/" + SupplierId);
        }
    }
}
