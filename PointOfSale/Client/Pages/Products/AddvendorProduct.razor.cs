using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using PointOfSale.DAL.ViewModels;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
namespace PointOfSale.Client.Pages.Products
{
    public partial class AddvendorProduct : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }


        [Parameter] public int? Id { get; set; }
        [Parameter] public int? VendorProductId { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<VendorProduct> formAddressType;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        VendorProduct _vendorProduct;
        protected VendorProduct vendorProduct
        {
            get
            {
                return _vendorProduct;
            }
            set
            {
                if (_vendorProduct != value)
                {
                    _vendorProduct = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        List<Vendor> vendors = new List<Vendor>();
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        protected async void Load()
        {
            vendors = await Http.GetFromJsonAsync<List<Vendor>>("/api/Vendors/GetAll/");
            if (VendorProductId != null)
            {
                vendorProduct = await Http.GetFromJsonAsync<VendorProduct>("/api/VendorProducts/GetById/" + VendorProductId);
            }
            else vendorProduct = new VendorProduct();
        }
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        protected async void FormAddressTypeSubmit(VendorProduct args)
        {
            try
            {
                vendorProduct.ProductId = (int)Id;
                if (vendorProduct.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<VendorProduct>("/api/VendorProducts/Update", vendorProduct))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<VendorProduct>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                    //    UriHelper.NavigateTo("/ProductPurchase/" + Id);
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<VendorProduct>("/api/VendorProducts/Insert", vendorProduct))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<VendorProduct>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                    //    UriHelper.NavigateTo("/ProductPurchase/" + Id);
                    }
                }
                DialogService.Close(vendorProduct);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Vendor Product!");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
