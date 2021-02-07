using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace PointOfSale.Client.Pages.Suppliers
{
    public partial class DialogCardPage : ComponentBase
    {
        [Parameter] public int? id { get; set; }
        [Parameter] public int? SupplierContactId { get; set; }
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
        SupplierContact Suppliercontact = new SupplierContact();
        protected override async Task OnInitializedAsync()
        {
            getContactTypesResult = await Http.GetFromJsonAsync<List<ContactType>>("api/ContactTypes/GetAll");
            Suppliercontact.SupplierId = (int)id;
            if (SupplierContactId != null)
            {
                Suppliercontact = await Http.GetFromJsonAsync<SupplierContact>("api/SupplierContacts/GetById/" + SupplierContactId);
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
                        var postResult = await Http.PostAsync("/api/Suppliers/Upload", content);
                        var postContent = await postResult.Content.ReadAsStringAsync();
                        if (!postResult.IsSuccessStatusCode)
                        {
                            throw new ApplicationException(postContent);
                        }
                        else
                        {
                            ImgUrl = postContent;
                            Suppliercontact.ContactImage = ImgUrl;
                        }
                    }
                }
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        protected async void SuppliercontactFormSubmit(SupplierContact args)
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            try
            {
                if (Suppliercontact.Id != 0)
                {
                    var sahlErpCreateSupplierContactResult = await Http.PutAsJsonAsync<SupplierContact>("/api/SupplierContacts/Update", Suppliercontact);
                }
                else
                {
                    var sahlErpCreateSupplierContactResult = await Http.PostAsJsonAsync<SupplierContact>("/api/SupplierContacts/Insert", Suppliercontact);
                }
                DialogService.Close(Suppliercontact);
                await JSRuntime.InvokeVoidAsync("StopLoading");
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new SupplierContact! /n {ex}");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
