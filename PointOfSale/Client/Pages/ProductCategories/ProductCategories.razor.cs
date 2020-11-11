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

namespace PointOfSale.Client.Pages.ProductCategories
{
    public partial class ProductCategories : ComponentBase
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

        protected RadzenGrid<ProductCategory> grid0;

        protected RadzenButton gridDeleteButton;

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
            var sahlErpGetProductCategoriesResult = await Http.GetFromJsonAsync<List<ProductCategory>>("/api/ProductCategories/GetAll");
            getProductCategoriesResult = sahlErpGetProductCategoriesResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddProductCategory>("Add Product Category", null);
           await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void EditRow(ProductCategory args)
        {
            DialogService.Open<AddProductCategory>("Edit Product Category", new Dictionary<string, object>() { { "Id", args.Id } },
                      new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, ProductCategory data)
        {
            try
            {
                var sahlErpDeleteProductCategoryResult = await Http.DeleteAsync("/api/ProductCategories/Delete/" + data.Id);
                Load();
                //var sahlErpDeleteProductCategoryResult = await SahlErp.DeleteProductCategory(data.Id);
                if (sahlErpDeleteProductCategoryResult != null)
                {
                  await  grid0.Reload();
                }
            }
            catch (Exception sahlErpDeleteProductCategoryException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete ProductCategory");
            }
        }
    }
}
