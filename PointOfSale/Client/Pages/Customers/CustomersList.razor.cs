using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Customers
{
    public partial class CustomersList : ComponentBase
    {
        private string searchTerm { get; set; } = "";
        List<Customer> Model => Customers.Where(i => i.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        List<Customer> Customers = new List<Customer>();
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Customers = await Http.GetFromJsonAsync<List<Customer>>("/api/Customers/GetAll");
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        async void DisActive(int customerId)
        {
            await Http.GetFromJsonAsync<bool>("/api/Customers/DisActive/"+ customerId);
        }
    }
}
