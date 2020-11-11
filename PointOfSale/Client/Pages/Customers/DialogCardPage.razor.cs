using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointOfSale.DAL.Domains;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http;
using Tewr.Blazor.FileReader;
using System.Net.Http.Headers;
using Radzen.Blazor;
using Radzen;
using Microsoft.AspNetCore.Components.Web;
using PointOfSale.DAL.ViewModels;
using Microsoft.JSInterop;

namespace PointOfSale.Client.Pages.Customers
{
    public partial class DialogCardPage : ComponentBase
    {
        [Parameter] public int? id { get; set; }
        [Parameter] public int? CustomerContactId { get; set; }
        [Parameter]
        public string ImgUrl { get; set; }
        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public IFileReaderService FileReaderService { get; set; }
        List<ContactType> getContactTypesResult = new List<ContactType>();
        private ElementReference _input;
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        CustomerContact customercontact = new CustomerContact();
        protected override async Task OnInitializedAsync()
        {
            getContactTypesResult = await Http.GetFromJsonAsync<List<ContactType>>("api/ContactTypes/GetAll");
            customercontact.CustomerId = (int)id;
            if (CustomerContactId != null)
            {
                customercontact = await Http.GetFromJsonAsync<CustomerContact>("api/CustomerContacts/GetById/" + CustomerContactId);
            }
        }
        private async Task HandleSelected()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            foreach (var file in await FileReaderService.CreateReference(_input).EnumerateFilesAsync())
            {
                if (file != null)
                {
                    var fileInfo = await file.ReadFileInfoAsync();
                    using (var ms = await file.CreateMemoryStreamAsync(4 * 1024))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, Convert.ToInt32(ms.Length)), "image", fileInfo.Name);
                        var postResult = await Http.PostAsync("/api/Customers/Upload", content);
                        var postContent = await postResult.Content.ReadAsStringAsync();
                        if (!postResult.IsSuccessStatusCode)
                        {
                            throw new ApplicationException(postContent);
                        }
                        else
                        {
                            ImgUrl = postContent;
                            customercontact.ContactImage = ImgUrl;
                        }
                    }
                }
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        protected async void customercontactFormSubmit(CustomerContact args)
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            try
            {
                if (customercontact.Id != 0)
                {
                    var sahlErpCreateCustomerContactResult = await Http.PutAsJsonAsync<CustomerContact>("/api/CustomerContacts/Update",  customercontact );
                }
                else
                {
                    var sahlErpCreateCustomerContactResult = await Http.PostAsJsonAsync<CustomerContact>("/api/CustomerContacts/Insert", customercontact );
                }
                DialogService.Close(customercontact);
                await JSRuntime.InvokeVoidAsync("StopLoading");
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new CustomerContact! /n {ex}");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
