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

namespace PointOfSale.Client.Pages.ContactTypes
{
    public partial class AddContactType : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }
       
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        
        [Parameter] public int? Id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<ContactType> formContactType;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        ContactType _ContactType;
        protected ContactType ContactType
        {
            get
            {
                return _ContactType;
            }
            set
            {
                if (_ContactType != value)
                {
                    _ContactType = value;
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
                ContactType = await Http.GetFromJsonAsync<ContactType>("/api/ContactTypes/GetById/" + Id);
            }
            else ContactType = new ContactType();
        }

        protected async void FormContactTypeSubmit(ContactType args)
        {
            try
            {
                if (ContactType.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<ContactType>("/api/ContactTypes/Update", ContactType))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<ContactType>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/ContactTypesList");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<ContactType>("/api/ContactTypes/Insert", ContactType))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<ContactType>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/ContactTypesList");
                    }
                }
                DialogService.Close(ContactType);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new ContactType!");
            }
        }

        protected void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
