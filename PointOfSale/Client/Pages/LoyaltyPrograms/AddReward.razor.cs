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

namespace PointOfSale.Client.Pages.LoyaltyPrograms
{
    public partial class AddReward : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Parameter] public int? Id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<Reward> formReward;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;
        int applyDiscountType;
        IEnumerable<DropDownList> applyDiscountTypes = Enum.GetValues(typeof(ApplyDiscountType)).Cast<ApplyDiscountType>().Select(x => new DropDownList
        {
            Id = (int)x,
            Name = x.ToString()
        });
        Reward _Reward;
        protected Reward Reward
        {
            get
            {
                return _Reward;
            }
            set
            {
                if (_Reward != value)
                {
                    _Reward = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        List<Product> _giftProducts;
        protected List<Product> giftProducts
        {
            get
            {
                return _giftProducts;
            }
            set
            {
                if (_giftProducts != value)
                {
                    _giftProducts = value;
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
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();

        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }

        protected async void Load()
        {

            giftProducts= await Http.GetFromJsonAsync<List<Product>>("/api/Products/GetAll/");
            if (Id != null)
            {
                Reward = await Http.GetFromJsonAsync<Reward>("/api/Rewards/GetById/" + Id);
            }
            else Reward = new Reward();
        }
        public void RadioSelection(object val)
        {
            Reward.RewardType = (RewardType)val;
        }
        protected async void FormRewardSubmit(Reward args)
        {
            try
            {
                Reward.ApplyDiscountType =(ApplyDiscountType) applyDiscountType;
                if (Reward.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<Reward>("/api/Rewards/Update", Reward))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<Reward>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/RewardsList");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<Reward>("/api/Rewards/Insert", Reward))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<Reward>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/RewardsList");
                    }
                }
                DialogService.Close(Reward);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Reward!");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
