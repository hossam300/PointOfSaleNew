using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Suppliers
{
    public partial class AddSupplierConacts : ComponentBase
    {

        [Parameter]
        public int id { get; set; }
        protected RadzenButton btnAddSupplierContact;
        RadzenGrid<SupplierContact> SupplierContactGrid;
        List<SupplierContact> SupplierContacts;
        SupplierContact SupplierContact;
        IEnumerable<AddressType> addressTypes;
        IEnumerable<ContactType> ContactTypes;
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            ContactTypes = await Http.GetFromJsonAsync<List<ContactType>>("api/ContactTypes/GetAll");
            addressTypes = await Http.GetFromJsonAsync<List<AddressType>>("api/AddressTypes/GetAll");
            SupplierContact = new SupplierContact();
            SupplierContacts = await Getdata();
            dialogService.OnOpen += Open;
            dialogService.OnClose += Close;
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }

        private async Task<List<SupplierContact>> Getdata()
        {
            return await Http.GetFromJsonAsync<List<SupplierContact>>("api/SupplierContacts/GetSupplierContactBuSupplierId/" + id);
        }


        public async void btnAddSupplierContactClick()
        {
            dialogService.Open<DialogCardPage>(Loc["AddSupplierContact"],
                         new Dictionary<string, object>() { { "id", id } },
                         new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await SupplierContactGrid.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }


        async void EditRow(SupplierContact SupplierContact)
        {
            dialogService.Open<DialogCardPage>(Loc["EditSupplierContact"], new Dictionary<string, object>() { { "id", id }, { "SupplierContactId", SupplierContact.Id } },
                         new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }





        async void DeleteRow(SupplierContact SupplierContact)
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            if (SupplierContacts.Contains(SupplierContact))
            {
                var sahlErpCreateSupplierContactResult = await Http.DeleteAsync("/api/SupplierContacts/Delete/" + SupplierContact.Id);
                //dbContext.Remove<Order>(order);

                //// For demo purposes only
                //orders.Remove(order);

                // For production
                //dbContext.SaveChanges();
                SupplierContacts = await Getdata();
                await SupplierContactGrid.Reload();
                StateHasChanged();
            }
            else
            {
                SupplierContactGrid.CancelEditRow(SupplierContact);
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
            SupplierContacts = await Getdata();
            await SupplierContactGrid.Reload();
            StateHasChanged();
        }




    }
}
