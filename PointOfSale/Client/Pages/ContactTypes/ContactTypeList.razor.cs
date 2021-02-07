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

namespace PointOfSale.Client.Pages.ContactTypes
{
    public partial class ContactTypeList : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }



        protected RadzenContent content1;

        protected RadzenButton btnContactTypes;

        protected RadzenGrid<ContactType> gridContactTypes;

        protected RadzenButton gridDeleteButton;

        IEnumerable<ContactType> _ContactTypes;
        protected IEnumerable<ContactType> ContactTypes
        {
            get
            {
                return _ContactTypes;
            }
            set
            {
                if (_ContactTypes != value)
                {
                    _ContactTypes = value;
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
            await gridContactTypes.Reload();
            StateHasChanged();
        }
        protected async void Load()
        {
            var sahlErpGetContactTypesResult = await Http.GetFromJsonAsync<List<ContactType>>("/api/ContactTypes/GetAll");

            ContactTypes = sahlErpGetContactTypesResult;
        }

        protected async void btnContactTypesClick(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddContactType>("Add Address Type", null);
            await gridContactTypes.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void EditRow(ContactType args)
        {
            DialogService.Open<AddContactType>("Edit Address Type", new Dictionary<string, object>() { { "Id", args.Id } },
                      new Radzen.DialogOptions() { Width = "700px", Height = "530px" });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, ContactType data)
        {
            try
            {
                var sahlErpGetContactTypesResult = await Http.DeleteAsync("/api/ContactTypes/Delete/" + data.Id);
                Load();
                if (sahlErpGetContactTypesResult != null)
                {
                    await gridContactTypes.Reload();
                }
            }
            catch (Exception sahlErpDeleteContactTypeException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete ContactType");
            }
        }
    }
}
