using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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

namespace PointOfSale.Client.Pages.PurchaseOrders
{
    public partial class AddPurchaseOrder : ComponentBase
    {
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        public RadzenGrid<PurchaseOrderItem> gridPurchaseOrders;

        //[Inject]
        //protected HttpClient Http { get; set; }
        //[Inject]
        //protected JSRuntime JSRuntime { get; set; }
        List<DropDownList> Suppliers = new List<DropDownList>();
        [Parameter] public int? Id { get; set; }
        [Parameter] public int? ShopId { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<PurchaseOrder> formPurchaseOrder;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;

        PurchaseOrder _PurchaseOrder;
        protected PurchaseOrder PurchaseOrder
        {
            get
            {
                return _PurchaseOrder;
            }
            set
            {
                if (_PurchaseOrder != value)
                {
                    _PurchaseOrder = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        public int OrderNumber { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Load();
            DialogService.OnOpen += Open;
            DialogService.OnClose += Close;
            //    DialogService.OnClose += CloseInstallment;
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        protected async void Load()
        {
            Suppliers = await Http.GetFromJsonAsync<List<DropDownList>>("/api/Suppliers/GetDropDownListAll/");
            if (Id != null)
            {
                PurchaseOrder = await Http.GetFromJsonAsync<PurchaseOrder>("/api/PurchaseOrders/GetById/" + Id);
            }
            else
            {
                PurchaseOrder = new PurchaseOrder();
                OrderNumber = await Http.GetFromJsonAsync<int>("/api/PurchaseOrders/GetOrderNumber/");
            }
            await InvokeAsync(() => { StateHasChanged(); });
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        public async void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
            events.Add(DateTime.Now, "Dialog opened");
            await InvokeAsync(() => { StateHasChanged(); });
        }
        //
        public async void Close(dynamic result)
        {

            events.Add(DateTime.Now, "Dialog closed. Result: " + result);
            if (result != null)
            {
                PurchaseOrder.PurchaseOrderItem.Add(result as PurchaseOrderItem);
                await gridPurchaseOrders.Reload();
                //customers = await LoadCustomers();
                //order = new Order();
                //orderItems = new List<OrderItem>();
                //UpdateTotals();
            }
            await InvokeAsync(() => { StateHasChanged(); });
        }
        protected async void FormPurchaseOrderSubmit(PurchaseOrder args)
        {
            PurchaseOrder.OrderNumber = OrderNumber.ToString();
            if (PurchaseOrder.OrderNumber == "0" || PurchaseOrder.OrderNumber == null || PurchaseOrder.OrderNumber == "")
            {
                await JSRuntime.InvokeVoidAsync("requied", "رقم الطلب مطلوب");
            }
            else if (PurchaseOrder.SupplierId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "المورد مطلوب");
            }
            else
            {
                try
                {
                    PurchaseOrder.OrderNumber = OrderNumber.ToString();
                    PurchaseOrder.ShopId = (int)ShopId;
                    PurchaseOrder.OrderDate = DateTime.Now;
                    PurchaseOrder.OrderType = OrderType.Payed;
                    PurchaseOrder.CreationDate = DateTime.Now;
                    var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    var user = authState.User;
                    var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                    PurchaseOrder.CreatorId = users;
                    PurchaseOrder.Total = PurchaseOrder.PurchaseOrderItem.Sum(x => (x.Price * x.Quantity) - x.ProductDiscount);
                    PurchaseOrder.TotalItemCount = PurchaseOrder.PurchaseOrderItem.Sum(x => x.Quantity);
                    foreach (var item in PurchaseOrder.PurchaseOrderItem)
                    {
                        item.Product = null;
                    }
                    if (PurchaseOrder.Id != 0)
                    {
                        using (var response = await Http.PutAsJsonAsync<PurchaseOrder>("/api/PurchaseOrders/Update", PurchaseOrder))
                        {
                            // convert response data to JsonElement which can handle any JSON data
                            var data = await response.Content.ReadFromJsonAsync<PurchaseOrder>();

                            // get id property from JSON response data
                            //  var customerId = data.Id;
                            UriHelper.NavigateTo("/PurchaseOrderList");
                        }
                    }
                    else
                    {

                        using (var response = await Http.PostAsJsonAsync<PurchaseOrder>("/api/PurchaseOrders/InsertPurchaseOrders", PurchaseOrder))
                        {
                            // convert response data to JsonElement which can handle any JSON data
                            var data = await response.Content.ReadFromJsonAsync<PurchaseOrder>();

                            // get id property from JSON response data
                            //  var customerId = data.Id;
                            UriHelper.NavigateTo("/PurchaseOrderList");
                        }
                    }
                    DialogService.Close(PurchaseOrder);
                }
                catch (Exception ex)
                {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new PurchaseOrder!");
                }
            }
        }
        public async Task OpenInstalmentAsync()
        {
            PurchaseOrder.OrderNumber = OrderNumber.ToString();
            if (PurchaseOrder.OrderNumber == "0" || PurchaseOrder.OrderNumber == null || PurchaseOrder.OrderNumber == "")
            {
                await JSRuntime.InvokeVoidAsync("requied", "رقم الطلب مطلوب");
            }
            else if (PurchaseOrder.SupplierId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "المورد مطلوب");
            }
            else
            {
                try
                {

                    PurchaseOrder.ShopId = (int)ShopId;
                    PurchaseOrder.OrderDate = DateTime.Now;
                    PurchaseOrder.OrderType = OrderType.Payed;
                    PurchaseOrder.CreationDate = DateTime.Now;
                    var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    var user = authState.User;
                    var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                    PurchaseOrder.CreatorId = users;
                    PurchaseOrder.Total = PurchaseOrder.PurchaseOrderItem.Sum(x => (x.Price * x.Quantity) - x.ProductDiscount);
                    PurchaseOrder.TotalItemCount = PurchaseOrder.PurchaseOrderItem.Sum(x => x.Quantity);
                    foreach (var item in PurchaseOrder.PurchaseOrderItem)
                    {
                        item.Product = null;
                    }
                    if (PurchaseOrder.Id != 0)
                    {
                        using (var response = await Http.PutAsJsonAsync<PurchaseOrder>("/api/PurchaseOrders/Update", PurchaseOrder))
                        {
                            //// convert response data to JsonElement which can handle any JSON data
                            var data = await response.Content.ReadFromJsonAsync<PurchaseOrder>();

                            //// get id property from JSON response data
                            PurchaseOrder.Id = data.Id;
                            //UriHelper.NavigateTo("/PurchaseOrderList");
                        }
                    }
                    else
                    {

                        using (var response = await Http.PostAsJsonAsync<PurchaseOrder>("/api/PurchaseOrders/Insert", PurchaseOrder))
                        {
                            //// convert response data to JsonElement which can handle any JSON data
                            var data = await response.Content.ReadFromJsonAsync<PurchaseOrder>();

                            //// get id property from JSON response data
                            PurchaseOrder.Id = data.Id;
                            //UriHelper.NavigateTo("/PurchaseOrderList");
                        }
                    }

                    // DialogService.Close(PurchaseOrder);
                }
                catch (Exception ex)
                {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new PurchaseOrder!");
                }
                var result = await DialogService.OpenAsync<AddPurchaseOrderInstallment>("اضافة دفعات التسديد", new Dictionary<string, object>() { { "PurchaseOrder", PurchaseOrder } },
                          new Radzen.DialogOptions() { Width = "800px", Height = "325px" });
                //await gridPurchaseOrders.Reload();

                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        public void PayWithMuiltMethod()
        {

        }
        public void PayCash()
        {

        }
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        void EditRow(PurchaseOrderItem purchaseOrderItem)
        {
            gridPurchaseOrders.EditRow(purchaseOrderItem);
        }

        void OnUpdateRow(PurchaseOrderItem purchaseOrderItem)
        {
            //dbContext.Update(order);

            //// For demo purposes only
            //order.Customer = dbContext.Customers.Find(order.CustomerID);
            //order.Employee = dbContext.Employees.Find(order.EmployeeID);

            // For production
            //dbContext.SaveChanges();
        }

        void SaveRow(PurchaseOrderItem purchaseOrderItem)
        {
            gridPurchaseOrders.UpdateRow(purchaseOrderItem);
        }

        void CancelEdit(PurchaseOrderItem purchaseOrderItem)
        {
            gridPurchaseOrders.CancelEditRow(purchaseOrderItem);

            // For production
            //var orderEntry = dbContext.Entry(order);
            //if (orderEntry.State == EntityState.Modified)
            //{
            //    orderEntry.CurrentValues.SetValues(orderEntry.OriginalValues);
            //    orderEntry.State = EntityState.Unchanged;
            //}
        }

        void DeleteRow(PurchaseOrderItem purchaseOrderItem)
        {
            //if (orders.Contains(order))
            //{
            //    dbContext.Remove<Order>(order);

            //    // For demo purposes only
            //    orders.Remove(order);

            //    // For production
            //    //dbContext.SaveChanges();

            //    ordersGrid.Reload();
            //}
            //else
            //{
            //    ordersGrid.CancelEditRow(order);
            //}
        }
        protected async void btnAddressTypesClick(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddPurchaseOrderItem>("اضافة منتج للفاتورة", null);
            await gridPurchaseOrders.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }
        void InsertRow()
        {
            gridPurchaseOrders.InsertRow(new PurchaseOrderItem());
        }

        void OnCreateRow(PurchaseOrderItem purchaseOrderItem)
        {
            //dbContext.Add(order);

            //// For demo purposes only
            //order.Customer = dbContext.Customers.Find(order.CustomerID);
            //order.Employee = dbContext.Employees.Find(order.EmployeeID);

            // For production
            //dbContext.SaveChanges();
        }
        protected void btnCancelClick(MouseEventArgs args)
        {
            UriHelper.NavigateTo("/PurchaseOrderList");
        }
    }
}
