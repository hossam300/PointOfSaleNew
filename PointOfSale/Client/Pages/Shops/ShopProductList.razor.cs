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
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Shops
{
    public partial class ShopProductList : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        List<DropDownList> Products = new List<DropDownList>();


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<ShopProductDTO> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<ShopProductDTO> _getShopsResult;
        [Parameter] public int? Id { get; set; }
        protected IEnumerable<ShopProductDTO> getShopsResult
        {
            get
            {
                return _getShopsResult;
            }
            set
            {
                if (_getShopsResult != value)
                {
                    _getShopsResult = value;
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


        async void Close(dynamic result)
        {
            //events.Add(DateTime.Now, "Dialog closed. Result: " + result);
            Load();
            await grid0.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            var sahlErpGetShopsResult = await Http.GetFromJsonAsync<List<ShopProductDTO>>("/api/ShopProducts/GetAllByShopId/" + Id);
            Products = await Http.GetFromJsonAsync<List<DropDownList>>("/api/Products/GetDropDownListAll/");
            getShopsResult = sahlErpGetShopsResult;
        }
        public async void SaveRow(ShopProductDTO args)
        {
            await grid0.UpdateRow(args);
        }
        void CancelEdit(ShopProductDTO shopProduct)
        {
            grid0.CancelEditRow(shopProduct);

        }

        protected async void Button0Click(MouseEventArgs args)
        {
            UriHelper.NavigateTo("/Shops/AddShop");
        }

        protected async void EditRow(ShopProductDTO args)
        {
            grid0.EditRow(args);
        }
        public async Task OnUpdateRow(ShopProductDTO shopProduct)
        {
            await Http.PostAsJsonAsync<ShopProductDTO>("/api/ShopProducts/EditShopProduct/", shopProduct);
        }
        protected async void GridDeleteButtonClick(MouseEventArgs args, ShopProductDTO data)
        {
            try
            {
                var sahlErpDeleteShopResult = await Http.DeleteAsync("/api/ShopProducts/Delete/" + data.Id);
                if (sahlErpDeleteShopResult != null)
                {
                    Load();
                    await grid0.Reload();
                }
            }
            catch (Exception sahlErpDeleteShopException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Shop");
            }
        }
    }
}
