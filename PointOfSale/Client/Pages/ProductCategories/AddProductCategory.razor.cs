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
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace PointOfSale.Client.Pages.ProductCategories
{
    public partial class AddProductCategory : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public IFileReaderService FileReaderService { get; set; }
        public string ImgUrl { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }
        [Parameter] public int? Id { get; set; }
        private ElementReference _input;
        protected RadzenContent content1;

        protected RadzenTemplateForm<ProductCategory> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox imagePath;

        protected RadzenLabel label2;

        protected RadzenTextBox categoryName;

        protected RadzenLabel label3;

        protected RadzenDropDown<ProductCategory> parentCategoryId;

        protected RadzenRequiredValidator parentCategoryIdRequiredValidator;

        protected RadzenLabel label4;



        protected RadzenLabel label5;



        protected RadzenButton button1;

        protected RadzenButton button2;

        IEnumerable<ProductCategory> _getProductCategoriesResult;
        protected IEnumerable<ProductCategory> getProductCategoriesResult
        {
            get
            {
                return _getProductCategoriesResult;
            }
            set
            {
                if (_getProductCategoriesResult != value)
                {
                    _getProductCategoriesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
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
                            productcategory.ImagePath = ImgUrl;
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
        ProductCategory _productcategory;
        protected ProductCategory productcategory
        {
            get
            {
                return _productcategory;
            }
            set
            {
                if (_productcategory != value)
                {
                    _productcategory = value;
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
            var sahlErpGetProductCategoriesResult = await Http.GetFromJsonAsync<List<ProductCategory>>("/api/ProductCategories/GetAll");
            getProductCategoriesResult = sahlErpGetProductCategoriesResult;

            if (Id != null)
            {
                productcategory = await Http.GetFromJsonAsync<ProductCategory>("/api/ProductCategories/GetById/" + Id);
                ImgUrl = productcategory.ImagePath;
                await InvokeAsync(() => { StateHasChanged(); });
            }
            else
                productcategory = new ProductCategory();
        }

        protected async void Form0Submit(ProductCategory args)
        {
            try
            {
                if (productcategory.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<ProductCategory>("/api/ProductCategories/Update", productcategory))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<ProductCategory>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/ProductCategories");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<ProductCategory>("/api/ProductCategories/Insert", productcategory))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<ProductCategory>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/ProductCategories");
                    }
                }
                DialogService.Close(productcategory);
            }
            catch (Exception sahlErpCreateProductCategoryException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new ProductCategory!");
            }
        }

        protected async void Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
