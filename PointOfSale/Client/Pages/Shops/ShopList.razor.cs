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
    public partial class ShopList : ComponentBase
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

        protected RadzenGrid<ShopDTO> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<ShopDTO> _getShopsResult;
        protected IEnumerable<ShopDTO> getShopsResult
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
            var sahlErpGetShopsResult = await Http.GetFromJsonAsync<List<ShopDTO>>("/api/Shops/GetAllWithoutInclude");
            getShopsResult = sahlErpGetShopsResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            UriHelper.NavigateTo("/Shops/AddShop");
        }

        protected async void EditRow(ShopDTO args)
        {
            UriHelper.NavigateTo("/Shops/EditShop/"+ args.Id);
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, ShopDTO data)
        {
            try
            {
                var sahlErpDeleteShopResult = await Http.DeleteAsync("/api/Shops/Delete/" + data.Id);
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
