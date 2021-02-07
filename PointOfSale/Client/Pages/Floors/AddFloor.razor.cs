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

namespace PointOfSale.Client.Pages.Floors
{
    public partial class AddFloor : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }
        
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Parameter] public int? Id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<Floor> formFloor;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        Floor _Floor;
        protected Floor Floor
        {
            get
            {
                return _Floor;
            }
            set
            {
                if (_Floor != value)
                {
                    _Floor = value;
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
                Floor = await Http.GetFromJsonAsync<Floor>("/api/Floors/GetById/" + Id);
            }
            else Floor = new Floor();
        }

        protected async void FormFloorSubmit(Floor args)
        {
            try
            {
                if (Floor.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<Floor>("/api/Floors/Update", Floor))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<Floor>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/FloorsList");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<Floor>("/api/Floors/Insert", Floor))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<Floor>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/FloorsList");
                    }
                }
                DialogService.Close(Floor);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Floor!");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
