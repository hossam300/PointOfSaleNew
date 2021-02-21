using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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

namespace PointOfSale.Client.Pages.PurchaseOrders
{
    public partial class AddPurchaseOrderItem : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        //[Inject]
        //protected HttpClient Http { get; set; }
        //[Inject]
        //protected JSRuntime JSRuntime { get; set; }
        [Parameter] public int? Id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<PurchaseOrderItem> formPurchaseOrderItem;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        PurchaseOrderItem _PurchaseOrderItem;
        List<DropDownList> Products = new List<DropDownList>();
        protected PurchaseOrderItem PurchaseOrderItem
        {
            get
            {
                return _PurchaseOrderItem;
            }
            set
            {
                if (_PurchaseOrderItem != value)
                {
                    _PurchaseOrderItem = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        protected async void Load()
        {
            Products = await Http.GetFromJsonAsync<List<DropDownList>>("/api/Products/GetDropDownListAll/");
            if (Id != null)
            {
                PurchaseOrderItem = await Http.GetFromJsonAsync<PurchaseOrderItem>("/api/PurchaseOrderItems/GetById/" + Id);
            }

            else PurchaseOrderItem = new PurchaseOrderItem();
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        protected async void FormPurchaseOrderItemSubmit(PurchaseOrderItem args)
        {
            try
            {
                //if (PurchaseOrderItem.Id != 0)
                //{
                //    using (var response = await Http.PutAsJsonAsync<PurchaseOrderItem>("/api/PurchaseOrderItems/Update", PurchaseOrderItem))
                //    {
                //        // convert response data to JsonElement which can handle any JSON data
                //        var data = await response.Content.ReadFromJsonAsync<PurchaseOrderItem>();

                //        // get id property from JSON response data
                //        //  var customerId = data.Id;
                //        //   UriHelper.NavigateTo("/PurchaseOrderItemsList");
                //    }
                //}
                //else
                //{
                //    using (var response = await Http.PostAsJsonAsync<PurchaseOrderItem>("/api/PurchaseOrderItems/Insert", PurchaseOrderItem))
                //    {
                //        // convert response data to JsonElement which can handle any JSON data
                //        var data = await response.Content.ReadFromJsonAsync<PurchaseOrderItem>();

                //        // get id property from JSON response data
                //        //  var customerId = data.Id;
                //        //  UriHelper.NavigateTo("/PurchaseOrderItemsList");
                //    }
                // }
                PurchaseOrderItem.Product = new Product { Id = PurchaseOrderItem.ProductId, Name = Products.FirstOrDefault(x => x.Id == PurchaseOrderItem.ProductId).Name };
                PurchaseOrderItem.SubTotal = (PurchaseOrderItem.Price * PurchaseOrderItem.Quantity) - PurchaseOrderItem.ProductDiscount;
                DialogService.Close(PurchaseOrderItem);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new PurchaseOrderItem!");
            }
        }
       
        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
