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
    public partial class AddRule : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }
       
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }


        [Parameter] public int? Id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<Rules> formRules;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        Rules _Rules;
        protected Rules Rules
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

        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }

        protected async void Load()
        {
            if (Id != null)
            {
                Rules = await Http.GetFromJsonAsync<Rules>("/api/Rules/GetById/" + Id);
            }
            else Rules = new Rules();
        }

        protected async void FormRulesSubmit(Rules args)
        {
            try
            {
                if (Rules.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<Rules>("/api/Rules/Update", Rules))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<Rules>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/Rules");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<Rules>("/api/Rules/Insert", Rules))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<Rules>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/Rules");
                    }
                }
                DialogService.Close(Rules);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Rules!");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
