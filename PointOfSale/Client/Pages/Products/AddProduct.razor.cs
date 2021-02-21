using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace PointOfSale.Client.Pages.Products
{
    public partial class AddProduct : ComponentBase
    {
        private ElementReference _input;
        AddProductDTO product = new AddProductDTO();
        IEnumerable<CompanyDTO> companies = new List<CompanyDTO>();
      //  IEnumerable<CustomerTaxDTO> customerTaxes = new List<CustomerTaxDTO>();
       // List<TaxDTO> Taxes = new List<TaxDTO>();
        int productType;
        IEnumerable<DropDownList> productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().Select(x => new DropDownList
        {
            Id = (int)x,
            Name = x.ToString()
        });
      //  IEnumerable<int> multipleValues = new int[] { };
        List<ProductCategoryDTO> productCategory = new List<ProductCategoryDTO>();
        bool popup = false;
        [Inject]
        public IFileReaderService FileReaderService { get; set; }
        [Parameter] public int? id { get; set; }
        [Parameter]
        public string ImgUrl { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            await Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        async Task Load()
        {
            companies = await Http.GetFromJsonAsync<List<CompanyDTO>>("/api/Companies/GetAllCompanyDTO");
            //Taxes = await Http.GetFromJsonAsync<List<TaxDTO>>("/api/Taxs/GetAllTaxDTO");
            //customerTaxes = Taxes.Select(x => new CustomerTaxDTO
            //{
            //    TaxId = x.Id,
            //    TaxName = x.Name
            //}).ToList();
            productCategory = await Http.GetFromJsonAsync<List<ProductCategoryDTO>>("/api/ProductCategories/GetAllProductCategoryDTO");
           // multipleValues = new List<int>();
            if (id != null)
            {
                product = await Http.GetFromJsonAsync<AddProductDTO>("api/Products/GetAddProductDTOById/" + id);
                ImgUrl = product.ProductImage;
                productType = (int)product.ProductType;
               // multipleValues = product.CustomerTaxes.Select(x => x.TaxId).ToList();
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
                            product.ProductImage = ImgUrl;
                        }
                    }
                }
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        void ChangeCustomerTaxes(object value)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;


            events.Add(DateTime.Now, $"value changed to {str}");
            InvokeAsync(StateHasChanged);
        }

        void Log(string eventName, string value)
        {
            events.Add(DateTime.Now, $"{eventName}: {value}");
        }
        void changeBarCode(bool value, string name)
        {
            if (product.GenerateBarcode)
            {
                product.Barcode = Guid.NewGuid().ToString().Remove(8);
                InvokeAsync(StateHasChanged);
            }
            else
            {
                product.Barcode = "";
                InvokeAsync(StateHasChanged);
            }
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        public async void FormProductSubmit(AddProductDTO model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));
            //bool formIsValid = model.Validate();
            if (product.Id == 0)
            {
                //foreach (var item in multipleValues)
                //{
                //    product.CustomerTaxes.Add(new CustomerTaxDTO { TaxId = item });
                //}
                product.ProductType = (ProductType)productType;
                int productId = 0;
                using (var response = await Http.PostAsJsonAsync<AddProductDTO>("/api/Products/InsertAddProductDTO", product))
                {
                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Product>();

                    // get id property from JSON response data
                    productId = data.Id;
                   
                }
                uriHelper.NavigateTo("/ProductSales/" + productId);

            }
            else
            {
                product.CustomerTaxes = new List<CustomerTaxDTO>();
                //product.Company = null;
                //foreach (var item in multipleValues)
                //{
                //    product.CustomerTaxes.Add(new CustomerTaxDTO { TaxId = item, ProductId = (int)id });
                //}
                product.ProductType = (ProductType)productType;
                product.Company = null;
                //foreach (var item in product.OptionalProducts)
                //{
                //    item.Product.Company = null;
                //}
                using (var response = await Http.PostAsJsonAsync<AddProductDTO>("/api/Products/UpdateAddProductDTO", product))
                {

                    // convert response data to JsonElement which can handle any JSON data
                    var data = await response.Content.ReadFromJsonAsync<Product>();

                    // get id property from JSON response data
                    //  var customerId = data[0].Id;
                   
                }
                uriHelper.NavigateTo("/ProductSales/" + id);
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
    }
}
