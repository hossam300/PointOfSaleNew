using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.LoyaltyPrograms
{
    public partial class AddLoyaltyProgram : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

       
        [Parameter] public int? Id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<LoyaltyProgram> formLoyaltyProgram;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;
        protected RadzenGrid<Reward> gridRewards;
        protected RadzenGrid<Rules> gridRules;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Reward> _Rewards;
        protected IEnumerable<Reward> Rewards
        {
            get
            {
                return _Rewards;
            }
            set
            {
                if (_Rewards != value)
                {
                    _Rewards = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        IEnumerable<Rules> _Rules;
        protected IEnumerable<Rules> Rules
        {
            get
            {
                return _Rules;
            }
            set
            {
                if (_Rules != value)
                {
                    _Rules = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        LoyaltyProgram _LoyaltyProgram;
        protected LoyaltyProgram LoyaltyProgram
        {
            get
            {
                return _LoyaltyProgram;
            }
            set
            {
                if (_LoyaltyProgram != value)
                {
                    _LoyaltyProgram = value;
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
            var sahlErpGetRewardsResult = await Http.GetFromJsonAsync<List<Reward>>("/api/Rewards/GetAll");

            Rewards = sahlErpGetRewardsResult;
            var sahlErpGetRulesResult = await Http.GetFromJsonAsync<List<Rules>>("/api/Rules/GetAll");
            Rules = sahlErpGetRulesResult;
            if (Id != null)
            {
                LoyaltyProgram = await Http.GetFromJsonAsync<LoyaltyProgram>("/api/LoyaltyPrograms/GetById/" + Id);
            }
           
            else LoyaltyProgram = new LoyaltyProgram();
        }
        protected async void GridDeleteRewardButtonClick(MouseEventArgs args, Reward data)
        {
            try
            {
                var sahlErpGetAddressTypesResult = await Http.DeleteAsync("/api/Rewards/Delete/" + data.Id);
                Load();
                if (sahlErpGetAddressTypesResult != null)
                {
                    await gridRewards.Reload();
                }
            }
            catch (Exception sahlErpDeleteAddressTypeException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete AddressType");
            }
        }
        protected async void GridDeleteRulesButtonClick(MouseEventArgs args, Rules data)
        {
            try
            {
                var sahlErpGetAddressTypesResult = await Http.DeleteAsync("/api/Rules/Delete/" + data.Id);
                Load();
                if (sahlErpGetAddressTypesResult != null)
                {
                    await gridRules.Reload();
                }
            }
            catch (Exception sahlErpDeleteAddressTypeException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete AddressType");
            }
        }
        protected async void FormLoyaltyProgramSubmit(LoyaltyProgram args)
        {
            try
            {
                if (LoyaltyProgram.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<LoyaltyProgram>("/api/LoyaltyPrograms/Update", LoyaltyProgram))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<LoyaltyProgram>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/LoyaltyPrograms");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<LoyaltyProgram>("/api/LoyaltyPrograms/Insert", LoyaltyProgram))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<LoyaltyProgram>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/LoyaltyPrograms");
                    }
                }
                DialogService.Close(LoyaltyProgram);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new LoyaltyProgram!");
            }
        }
        protected async void btnRewardsClick(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddReward>("Add Reward", null, new Radzen.DialogOptions() { Width = "80%", Height = "80%" });
            await gridRewards.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }
        protected async void btnRulesClick(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddRule>("Add Rule", null, new Radzen.DialogOptions() { Width = "80%", Height = "80%" });
            await gridRewards.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }
        protected async void EditRewardRow(Reward args)
        {
            DialogService.Open<AddReward>("Edit Reward", new Dictionary<string, object>() { { "Id", args.Id } },
                      new Radzen.DialogOptions() { Width = "80%", Height = "80%" });
            await InvokeAsync(() => { StateHasChanged(); });
        }
      
        protected async void EditRulesRow(Rules args)
        {
            DialogService.Open<AddRule>("Edit Rule", new Dictionary<string, object>() { { "Id", args.Id } },
                      new Radzen.DialogOptions() { Width = "80%", Height = "80%" });
            await InvokeAsync(() => { StateHasChanged(); });
        }
        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
