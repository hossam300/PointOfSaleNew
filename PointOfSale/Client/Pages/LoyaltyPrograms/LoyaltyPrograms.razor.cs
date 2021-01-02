using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.LoyaltyPrograms
{
    public partial class LoyaltyPrograms : ComponentBase
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

        protected RadzenGrid<LoyaltyProgram> gridLoyaltyProgram;

        protected RadzenButton gridDeleteButton;

        IEnumerable<LoyaltyProgram> _getProductCategoriesResult;
        protected IEnumerable<LoyaltyProgram> getProductCategoriesResult
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
            await gridLoyaltyProgram.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            var sahlErpGetProductCategoriesResult = await Http.GetFromJsonAsync<List<LoyaltyProgram>>("/api/LoyaltyPrograms/GetAll");
            getProductCategoriesResult = sahlErpGetProductCategoriesResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddLoyaltyProgram>(Loc["AddLoyaltyProgram"], null, new Radzen.DialogOptions() { Width = "80%", Height = "80%" });
            await gridLoyaltyProgram.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void EditRow(LoyaltyProgram args)
        {
            DialogService.Open<AddLoyaltyProgram>(Loc["EditLoyaltyProgram"], new Dictionary<string, object>() { { "Id", args.Id } },
                      new Radzen.DialogOptions() { Width = "80%", Height = "80%" });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, LoyaltyProgram data)
        {
            try
            {
                var sahlErpDeleteProductCategoryResult = await Http.DeleteAsync("/api/LoyaltyPrograms/Delete/" + data.Id);
                Load();
                //var sahlErpDeleteProductCategoryResult = await SahlErp.DeleteProductCategory(data.Id);
                if (sahlErpDeleteProductCategoryResult != null)
                {
                    await gridLoyaltyProgram.Reload();
                }
            }
            catch (Exception sahlErpDeleteProductCategoryException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete ProductCategory");
            }
        }
    }
}
