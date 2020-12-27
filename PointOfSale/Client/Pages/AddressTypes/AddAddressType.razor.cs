using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using PointOfSale.DAL.ViewModels;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;

namespace PointOfSale.Pages
{
    public partial class AddAddressTypeComponent : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected JSRuntime JSRuntime { get; set; }
        [Parameter] public int? Id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<AddressType> formAddressType;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        AddressType _addresstype;
        protected AddressType addresstype
        {
            get
            {
                return _addresstype;
            }
            set
            {
                if (_addresstype != value)
                {
                    _addresstype = value;
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
                addresstype = await Http.GetFromJsonAsync<AddressType>("/api/AddressTypes/GetById/" + Id);
            }
            else addresstype = new AddressType();
        }

        protected async void FormAddressTypeSubmit(AddressType args)
        {
            try
            {
                if (addresstype.Id != 0)
                {
                    using (var response = await Http.PutAsJsonAsync<AddressType>("/api/AddressTypes/Update", addresstype))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<AddressType>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/AddressTypesList");
                    }
                }
                else
                {
                    using (var response = await Http.PostAsJsonAsync<AddressType>("/api/AddressTypes/Insert", addresstype))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<AddressType>();

                        // get id property from JSON response data
                        //  var customerId = data.Id;
                        UriHelper.NavigateTo("/AddressTypesList");
                    }
                }
                DialogService.Close(addresstype);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new AddressType!");
            }
        }

        protected  void btnCancelClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
