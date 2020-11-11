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

namespace PointOfSale.Client.Pages.PaymentMethods
{
    public partial class AddPaymentMethod : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Parameter] public int? Id { get; set; }


        protected RadzenContent content1;

        protected RadzenTemplateForm<PaymentMethod> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox mehtodName;

        protected RadzenLabel label2;

        protected RadzenCheckBox<bool> cash;

        protected RadzenRequiredValidator cashRequiredValidator;

        protected RadzenLabel label3;


        protected RadzenButton button1;

        protected RadzenButton button2;

      

        PaymentMethod _paymentmethod;
        protected PaymentMethod paymentmethod
        {
            get
            {
                return _paymentmethod;
            }
            set
            {
                if (_paymentmethod != value)
                {
                    _paymentmethod = value;
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
                paymentmethod = await Http.GetFromJsonAsync<PaymentMethod>("/api/PaymentMethods/GetById/" + Id);
            }
            else

            paymentmethod = new PaymentMethod();
        }

        protected async void Form0Submit(PaymentMethod args)
        {
            try
            {
                if (paymentmethod.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<PaymentMethod>("/api/PaymentMethods/Update", paymentmethod))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<PaymentMethod>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/PaymentMethods");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<PaymentMethod>("/api/PaymentMethods/Insert", paymentmethod))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<PaymentMethod>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/PaymentMethods");
                    }
                }
                DialogService.Close(paymentmethod);
            }
            catch (Exception sahlErpCreatePaymentMethodException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new PaymentMethod!");
            }
        }

        protected async void Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
