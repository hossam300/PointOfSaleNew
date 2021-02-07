using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.PurchaseOrders
{
    public partial class AddPurchaseOrder : ComponentBase
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

        protected RadzenTemplateForm<PurchaseOrder> formPurchaseOrder;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        PurchaseOrder _PurchaseOrder;
        protected PurchaseOrder PurchaseOrder
        {
            get
            {
                return _PurchaseOrder;
            }
            set
            {
                if (_PurchaseOrder != value)
                {
                    _PurchaseOrder = value;
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
            if (Id != null)
            {
                PurchaseOrder = await Http.GetFromJsonAsync<PurchaseOrder>("/api/PurchaseOrders/GetById/" + Id);
            }
            else PurchaseOrder = new PurchaseOrder();
        }

        protected async void FormPurchaseOrderSubmit(PurchaseOrder args)
        {
            try
            {
                if (PurchaseOrder.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<PurchaseOrder>("/api/PurchaseOrders/Update", PurchaseOrder))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<PurchaseOrder>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/PurchaseOrdersList");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<PurchaseOrder>("/api/PurchaseOrders/Insert", PurchaseOrder))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<PurchaseOrder>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/PurchaseOrdersList");
                    }
                }
                DialogService.Close(PurchaseOrder);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new PurchaseOrder!");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
