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
    public partial class AddPurchaseOrderInstallment : ComponentBase
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
        [Parameter] public PurchaseOrder PurchaseOrder { get; set; }
        protected RadzenContent content1;
        public RadzenGrid<PurchaseOrderInstallment> gridPurchaseOrderPurchaseOrderInstallment;
        protected RadzenTemplateForm<PurchaseOrderInstallment> formPurchaseOrderInstallment;
        public List<PurchaseOrderInstallment> PurchaseOrderInstallments { get; set; }
        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        PurchaseOrderInstallment _PurchaseOrderInstallment;
        protected PurchaseOrderInstallment PurchaseOrderInstallment
        {
            get
            {
                return _PurchaseOrderInstallment;
            }
            set
            {
                if (_PurchaseOrderInstallment != value)
                {
                    _PurchaseOrderInstallment = value;
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
            PurchaseOrderInstallments = new List<PurchaseOrderInstallment>();
            if (Id != null)
            {
                PurchaseOrderInstallment = await Http.GetFromJsonAsync<PurchaseOrderInstallment>("/api/PurchaseOrderInstallments/GetById/" + Id);
            }

            else
            {
                PurchaseOrderInstallment = new PurchaseOrderInstallment();
                PurchaseOrderInstallment.InstallmentAmount = PurchaseOrder.PurchaseOrderItem.Sum(x => x.SubTotal);
                PurchaseOrderInstallment.InstallmentDate = DateTime.Now;
            };
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        public async void AddPurchaseOrderInstallmentToGrid()
        {
            PurchaseOrderInstallment.PurchaseOrder = PurchaseOrder;
            PurchaseOrderInstallment.PurchaseOrderId = PurchaseOrder.Id;
            try
            {
                PurchaseOrderInstallments.Add(PurchaseOrderInstallment);
                await gridPurchaseOrderPurchaseOrderInstallment.Reload();
            }
            catch (Exception ex)
            {

                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new PurchaseOrderInstallment!");
            }
           
            // DialogService.Close(PurchaseOrderInstallment);
        }
        protected async void FormPurchaseOrderInstallmentSubmit(PurchaseOrderInstallment args)
        {
            try
            {
                if (PurchaseOrderInstallment.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<PurchaseOrderInstallment>("/api/PurchaseOrderInstallments/Update", PurchaseOrderInstallment))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<PurchaseOrderInstallment>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        //   UriHelper.NavigateTo("/PurchaseOrderInstallmentsList");
                    }
                }
                else
                {
                    PurchaseOrderInstallment.PurchaseOrder = null;
                    using (var response = await Http.PostAsJsonAsync<PurchaseOrderInstallment>("/api/PurchaseOrderInstallments/Insert", PurchaseOrderInstallment))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<PurchaseOrderInstallment>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        //  UriHelper.NavigateTo("/PurchaseOrderInstallmentsList");
                    }
                }
                //PurchaseOrderInstallments.Add(PurchaseOrderInstallment);
                //await gridPurchaseOrderPurchaseOrderInstallment.Reload();
                UriHelper.NavigateTo("/PurchaseOrderList");
               // DialogService.Close(null);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new PurchaseOrderInstallment!");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
