using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointOfSale.DAL.Domains;
using Radzen.Blazor;
using System.Net.Http.Json;
using Radzen;
using Microsoft.JSInterop;

namespace PointOfSale.Client.Pages.Customers
{
    public partial class AddCustomerConacts : ComponentBase
    {

        [Parameter]
        public int id { get; set; }
        protected RadzenButton btnAddCustomerContact;
        RadzenGrid<CustomerContact> customerContactGrid;
        List<CustomerContact> customerContacts;
        CustomerContact customerContact;
        IEnumerable<AddressType> addressTypes;
        IEnumerable<ContactType> ContactTypes;
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            ContactTypes = await Http.GetFromJsonAsync<List<ContactType>>("api/ContactTypes/GetAll");
            addressTypes = await Http.GetFromJsonAsync<List<AddressType>>("api/AddressTypes/GetAll");
            customerContact = new CustomerContact();
            customerContacts = await Getdata();
            dialogService.OnOpen += Open;
            dialogService.OnClose += Close;
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }

        private async Task<List<CustomerContact>> Getdata()
        {
            return await Http.GetFromJsonAsync<List<CustomerContact>>("api/CustomerContacts/GetCustomerContactBuCustomerId/" + id);
        }


        public async void btnAddCustomerContactClick()
        {
            dialogService.Open<DialogCardPage>("Add Customer Contact",
                         new Dictionary<string, object>() { { "id", id } },
                         new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await customerContactGrid.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }


        async void EditRow(CustomerContact customerContact)
        {
            dialogService.Open<DialogCardPage>("Edit Customer Contact", new Dictionary<string, object>() { { "id", id }, { "CustomerContactId", customerContact.Id } },
                         new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }





        async void DeleteRow(CustomerContact customerContact)
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            if (customerContacts.Contains(customerContact))
            {
                var sahlErpCreateCustomerContactResult = await Http.DeleteAsync("/api/CustomerContacts/Delete/" + customerContact.Id);
                //dbContext.Remove<Order>(order);

                //// For demo purposes only
                //orders.Remove(order);

                // For production
                //dbContext.SaveChanges();
                customerContacts = await Getdata();
                await customerContactGrid.Reload();
                StateHasChanged();
            }
            else
            {
                customerContactGrid.CancelEditRow(customerContact);
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();


        void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
            events.Add(DateTime.Now, "Dialog opened");
            StateHasChanged();
        }

        async void Close(dynamic result)
        {
            events.Add(DateTime.Now, "Dialog closed. Result: " + result);
            customerContacts = await Getdata();
            await customerContactGrid.Reload();
            StateHasChanged();
        }




    }
}
