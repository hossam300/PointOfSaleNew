using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Products
{
    public partial class ProductPurchase : ComponentBase
    {
        RadzenGrid<VendorProduct> vendorProducGrid;
        Product product = new Product();
        List<VendorProduct> vendorProducs = new List<VendorProduct>();
        VendorProduct vendorProduct = new VendorProduct();
        [Parameter] public int id { get; set; }
        IEnumerable<VendorTaxDTO> vendorTaxes = new List<VendorTaxDTO>();
        List<Tax> Taxes = new List<Tax>();

        IEnumerable<int> multipleValues = new int[] { };
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            await Getdata();
            Taxes = await Http.GetFromJsonAsync<List<Tax>>("/api/Taxs/GetAll");
            vendorProducs = product.VendorProducts;
            vendorTaxes = Taxes.Select(x => new VendorTaxDTO
            {
                TaxId = x.Id,
                TaxName = x.Name
            }).ToList();
            multipleValues = product.VendorTaxes.Select(x => x.TaxId);
            dialogService.OnOpen += Open;
            dialogService.OnClose += Close;
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        async void EditRow(VendorProduct VendorProduct)
        {
            dialogService.Open<AddvendorProduct>("Edit Vendor Product", new Dictionary<string, object>() { { "id", id }, { "VendorProductId", VendorProduct.Id } },
                         new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }






        async void DeleteRow(VendorProduct VendorProduct)
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            if (vendorProducs.Contains(VendorProduct))
            {
                var sahlErpCreateCustomerContactResult = await Http.DeleteAsync("/api/VendorProducts/Delete/" + vendorProduct.Id);
                await Getdata();
                await vendorProducGrid.Reload();
                StateHasChanged();
            }
            else
            {
                vendorProducGrid.CancelEditRow(vendorProduct);
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }

        private async Task Getdata()
        {
            product = await Http.GetFromJsonAsync<Product>("api/Products/GetById/" + id);

        }
        void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
            events.Add(DateTime.Now, "Dialog opened");
            StateHasChanged();
        }

        async void Close(dynamic result)
        {
            events.Add(DateTime.Now, "Dialog closed. Result: " + result);
            //await Getdata();
            if (result != null)
            {
                var vendor = result as VendorProduct;
                product.VendorProducts.Add(new VendorProduct { VendorId = vendor.VendorId, Price = vendor.Price, ProductId = vendor.ProductId });
            }
            await vendorProducGrid.Reload();
            StateHasChanged();
        }
        public async void btnAddvendorProducClick()
        {
            dialogService.Open<AddvendorProduct>("Add vendor Product",
                         new Dictionary<string, object>() { { "id", id } },
                         new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await vendorProducGrid.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }
        public async void FormProductSubmit(Product model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            product.VendorTaxes = new List<VendorTax>();
            foreach (var item in multipleValues)
            {
                product.VendorTaxes.Add(new VendorTax { TaxId = item });
            }
            using (var response = await Http.PutAsJsonAsync<Product>("/api/Products/Update", product))
            {

                // convert response data to JsonElement which can handle any JSON data
                var data = await response.Content.ReadFromJsonAsync<Product>();

                // get id property from JSON response data
                //  var customerId = data[0].Id;
                uriHelper.NavigateTo("/ProductInventory/" + data.Id);
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}

