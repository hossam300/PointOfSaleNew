using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using Radzen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace PointOfSale.Client.Pages.Customers
{
    public partial class AddCustomer : ComponentBase
    {
        private ElementReference _input;
        Customer customer = new Customer();
        List<AddressType> addressTypes = new List<AddressType>();
        bool popup = false;
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public IFileReaderService FileReaderService { get; set; }
        [Parameter] public int? id { get; set; } 
        [Parameter] public bool? Modal { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            addressTypes = await Http.GetFromJsonAsync<List<AddressType>>("/api/AddressTypes/GetAll");
            
            if (id != null)
            {
                customer = await Http.GetFromJsonAsync<Customer>("api/Customers/GetById/" + id);
                ImgUrl = customer.CustomerImage;
            }
             await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        [Parameter]
        public string ImgUrl { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }
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
                            customer.CustomerImage = ImgUrl;
                        }
                    }
                }
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }

        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();

        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }

        void Log(string eventName, string value)
        {
            events.Add(DateTime.Now, $"{eventName}: {value}");
        }

        async void OnSubmit(Customer model)
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));
            //bool formIsValid = model.Validate();
            if (customer.Id == 0)
            {

                using (var response = await Http.PostAsJsonAsync<Customer>("/api/Customers/Insert",  customer ))
                {
                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Customer>();

                    // get id property from JSON response data
                    //  var customerId = data.Id;
                    if (Modal==true)
                    {
                        DialogService.Close(data);
                    }
                    uriHelper.NavigateTo("/AddCustomerConacts/" + data.Id);
                }
             
            }
            else
            {
                foreach (var item in customer.CustomerContacts)
                {
                    item.Customer = null;
                }
                using (var response = await Http.PutAsJsonAsync<Customer>("/api/Customers/Update", customer ))
                {
                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Customer>();

                    // get id property from JSON response data
                  //  var customerId = data[0].Id;
                    uriHelper.NavigateTo("/AddCustomerConacts/" + data.Id);
                }
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }

        void OnInvalidSubmit(Radzen.FormInvalidSubmitEventArgs args)
        {
            Log("InvalidSubmit", JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true }));
        }

        public void RadioSelection(object val)
        {
            customer.CustomerType = (CustomerType)val;
        }
    }
}
