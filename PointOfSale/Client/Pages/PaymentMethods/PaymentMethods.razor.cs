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
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.PaymentMethods
{
    public partial class PaymentMethods : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }



        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<PaymentMethod> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<PaymentMethod> _getPaymentMethodsResult;
        protected IEnumerable<PaymentMethod> getPaymentMethodsResult
        {
            get
            {
                return _getPaymentMethodsResult;
            }
            set
            {
                if (_getPaymentMethodsResult != value)
                {
                    _getPaymentMethodsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Load();
            DialogService.OnOpen += Open;
            DialogService.OnClose += Close;
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
            //events.Add(DateTime.Now, "Dialog opened");
            StateHasChanged();
        }

        async void Close(dynamic result)
        {
            //events.Add(DateTime.Now, "Dialog closed. Result: " + result);
            Load();
            await grid0.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            var sahlErpGetPaymentMethodsResult = await Http.GetFromJsonAsync<List<PaymentMethod>>("/api/PaymentMethods/GetAll");
            getPaymentMethodsResult = sahlErpGetPaymentMethodsResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddPaymentMethod>("Add Payment Method", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void EditRow(PaymentMethod args)
        {
            DialogService.Open<AddPaymentMethod>("Edit Payment Method", new Dictionary<string, object>() { { "Id", args.Id } },
                     new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, PaymentMethod data)
        {
            try
            {
                var sahlErpDeletePaymentMethodResult = await Http.DeleteAsync("/api/PaymentMethods/Delete/" + data.Id);
                if (sahlErpDeletePaymentMethodResult != null)
                {
                    Load();
                    await grid0.Reload();
                }
            }
            catch (Exception sahlErpDeletePaymentMethodException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete PaymentMethod");
            }
        }
    }
}
