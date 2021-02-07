
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace PointOfSale.Client.Pages.Suppliers
{
    public partial class AddSupplier : ComponentBase
    {
        private ElementReference _input;
        Supplier Supplier = new Supplier();
        List<AddressType> addressTypes = new List<AddressType>();
        bool popup = false;
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
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
                Supplier = await Http.GetFromJsonAsync<Supplier>("api/Suppliers/GetById/" + id);
                ImgUrl = Supplier.SupplierImage;
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
                        var postResult = await Http.PostAsync("/api/Suppliers/Upload", content);
                        var postContent = await postResult.Content.ReadAsStringAsync();
                        if (!postResult.IsSuccessStatusCode)
                        {
                            throw new ApplicationException(postContent);
                        }
                        else
                        {
                            ImgUrl = postContent;
                            Supplier.SupplierImage = ImgUrl;
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

        async void OnSubmit(Supplier model)
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));
            //bool formIsValid = model.Validate();
            Supplier.CreationDate = DateTime.Now;
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            Supplier.CreatorId = users;
            if (Supplier.Id == 0)
            {

                using (var response = await Http.PostAsJsonAsync<Supplier>("/api/Suppliers/Insert", Supplier))
                {
                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Supplier>();

                    // get id property from JSON response data
                    //  var SupplierId = data.Id;
                    if (Modal == true)
                    {
                        DialogService.Close(data);
                    }
                    else
                        uriHelper.NavigateTo("/AddSupplierConacts/" + data.Id);
                }

            }
            else
            {
                foreach (var item in Supplier.SupplierContacts)
                {
                    item.Supplier = null;
                }
                using (var response = await Http.PutAsJsonAsync<Supplier>("/api/Suppliers/Update", Supplier))
                {
                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Supplier>();

                    // get id property from JSON response data
                    //  var SupplierId = data[0].Id;
                    if (Modal == true)
                    {
                        DialogService.Close(data);
                    }
                    else
                        uriHelper.NavigateTo("/AddSupplierConacts/" + data.Id);
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
            Supplier.SupplierType = (CustomerType)val;
        }
    }
}
