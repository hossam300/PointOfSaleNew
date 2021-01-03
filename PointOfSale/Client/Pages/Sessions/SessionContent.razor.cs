using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Sessions
{
    public partial class SessionContent : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        //[Inject]
        //UserManager<SahlApplication> UserManager { get; set; }

        List<Product> Products = new List<Product>();
        List<Product> ProductCats = new List<Product>();
        List<OrderItem> orderItems = new List<OrderItem>();
        List<Customer> customers = new List<Customer>();
        List<ProductCategory> Categories = new List<ProductCategory>();
        List<ProductCategory> ProductCategories = new List<ProductCategory>();
        Radzen.Blazor.RadzenLabel CustomerVaildation;
        Radzen.Blazor.RadzenLabel ItemsVaildation;
        Radzen.Blazor.RadzenAutoComplete BarcodeId;
        Order order = new Order();
        int TotalItems = 0;
        double Total = 0;
        double Discount = 0;
        double OrderTax = 0;
        double TotalPayable = 0;
        int i = 1;
        [Inject] DialogService DialogService { get; set; }
        private Random random = new Random((int)DateTime.Now.Ticks);
        private string RandomString(int length)
        {
            const string pool = "0123456789";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            Products = await Http.GetFromJsonAsync<List<Product>>("/api/Products/GetAll");
            customers = await Http.GetFromJsonAsync<List<Customer>>("/api/Customers/GetAll");
            Categories = await Http.GetFromJsonAsync<List<ProductCategory>>("/api/ProductCategories/GetAll");
            ProductCats = GetProductsVyCategoryId(null);
            ProductCategories = GetProductCategories(0, 3, 0);
            DialogService.OnOpen += Open;
            DialogService.OnClose += Close;
            await BarcodeId.Element.FocusAsync();
            await JSRuntime.InvokeVoidAsync("Setfocus", "barcode2");
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            var sessions = await Http.GetFromJsonAsync<Session>("/api/Sessions/GetByShopId/" + Id);
            if (sessions.Id == 0)
            {
                Session session = new Session()
                {
                    SessionNo = RandomString(5),
                    CreationDate = DateTime.Now,
                    ShopId = Id,
                    Status = Status.Open,
                    CreatorId = users
                };
                await Http.PostAsJsonAsync<Session>("/api/Sessions/Insert", session);
            }
            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        List<Product> GetProductsVyCategoryId(int? CatId)
        {
            if (CatId != null)
                ProductCats = Products.Where(p => p.ProductCategoryId == CatId).ToList();
            else
                ProductCats = Products;

            return ProductCats;
        }
        List<ProductCategory> GetProductCategories(int start, int count, int preNext)
        {
            if (preNext == 1)
            {
                i--;
            }
            if (preNext == 2)
            {
                i++;
            }
            ProductCategories = Categories.Skip(start * count).Take(count).ToList();
            return ProductCategories;
        }
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        async void HoldOrder()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Customer Is Requird");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Choose One Product at Least");
            }
            else
            {
                order.Discount = Discount;
                order.OrderTax = OrderTax;
                order.Total = Total;
                order.TotalItemCount = TotalItems;
                order.CreationDate = DateTime.Now;
                order.OrderDate = DateTime.Now;
                order.ShopId = Id;
                order.Customer =new Customer { Id = order.CustomerId, Name = customers.FirstOrDefault(c => c.Id == order.CustomerId).Name };
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                order.CreatorId = users;
                order.OrderItem = new List<OrderItem>();
                order.OrderItem = orderItems;
                order.OrderType = OrderType.Draft;
                foreach (var item in order.OrderItem)
                {
                    item.Product.Category = null;
                    item.Product.ProductCategory = null;
                    item.Order = null;
                }
                var result = await DialogService.OpenAsync<HoldOrder>("Suspend Sale", new Dictionary<string, object>() { { "Order", order } },
                      new Radzen.DialogOptions() { Width = "500px", Height = "325px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        async void CancelOrder()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Customer Is Requird");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Choose One Product at Least");
            }
            else
            {
                order.Discount = Discount;
                order.OrderTax = OrderTax;
                order.Total = Total;
                order.TotalItemCount = TotalItems;
                order.CreationDate = DateTime.Now;
                order.OrderDate = DateTime.Now;
                order.ShopId = Id;
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                order.CreatorId = users;
                order.OrderItem = orderItems;
                order.OrderType = OrderType.Cancled;
                foreach (var item in order.OrderItem)
                {
                    item.Product.Category = null;
                    item.Product.ProductCategory = null;
                }
                var result = await DialogService.OpenAsync<CancelOrder>("Cancel Order", new Dictionary<string, object>() { { "Order", order } },
                        new Radzen.DialogOptions() { Width = "400px", Height = "250px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        async void PrintOrder()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Customer Is Requird");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Choose One Product at Least");
            }
            else
            {
                order.Discount = Discount;
                order.OrderTax = OrderTax;
                order.Total = Total;
                order.TotalItemCount = TotalItems;
                order.CreationDate = DateTime.Now;
                order.OrderDate = DateTime.Now;
                order.ShopId = Id;
                order.Customer = new Customer { Id = order.CustomerId, Name = customers.FirstOrDefault(c => c.Id == order.CustomerId).Name };
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                order.CreatorId = users;
                order.OrderItem = orderItems;
                var result = await DialogService.OpenAsync<PrintOrder>("Print Order", new Dictionary<string, object>() { { "Order", order } },
                        new Radzen.DialogOptions() { Width = "400px", Height = "500px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        async void PrintBill()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Customer Is Requird");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Choose One Product at Least");
            }
            else
            {
                order.Discount = Discount;
                order.OrderTax = OrderTax;
                order.Total = Total;
                order.TotalItemCount = TotalItems;
                order.CreationDate = DateTime.Now;
                order.OrderDate = DateTime.Now;
                order.ShopId = Id;
                order.Customer = new Customer { Id = order.CustomerId, Name = customers.FirstOrDefault(c => c.Id == order.CustomerId).Name };
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                order.CreatorId = users;
                order.OrderItem = orderItems;
                var result = await DialogService.OpenAsync<PrintBill>("Print Bill", new Dictionary<string, object>() { { "Order", order } },
                     new Radzen.DialogOptions() { Width = "400px", Height = "500px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        async void Payment()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Customer Is Requird");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "Choose One Product at Least");
            }
            else
            {
                order.Discount = Discount;
                order.OrderTax = OrderTax;
                order.Total = Total;
                order.TotalItemCount = TotalItems;
                order.CreationDate = DateTime.Now;
                order.OrderDate = DateTime.Now;
                order.ShopId = Id;
                //  order.Customer = customers.FirstOrDefault(c => c.Id == order.CustomerId);
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var users = user.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                order.CreatorId = users;
                order.OrderItem = orderItems;
                var result = await DialogService.OpenAsync<Payment>("Payment", new Dictionary<string, object>() { { "Order", order } },
                           new Radzen.DialogOptions() { Width = "600px", Height = "400px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }

        }
        async void AddCustomer()
        {
            var result = await DialogService.OpenAsync<Customers.AddCustomer>("Add Customer", new Dictionary<string, object>() { { "Modal", true } },
                      new Radzen.DialogOptions() { Width = "80%", Height = "80%" });
            await InvokeAsync(() => { StateHasChanged(); });
        }
        void Log(string eventName, string value)
        {
            events.Add(DateTime.Now, $"{eventName}: {value}");
        }
        void OnChange(object value, string name)
        {
            var product = Products.FirstOrDefault(c => c.Barcode == value.ToString() || c.Name == value.ToString());
            AddOrderItem(product, 1);
            BarcodeId.Value = "";
            Log($"{name} value changed to {value}", name);
        }
        async void ChangeOrderNo()
        {
            var Oldorder = await Http.GetFromJsonAsync<Order>("/api/Orders/GetOrderByNo/" + order.OrderNumber);

            if (Oldorder.Id != 0)
            {
                await JSRuntime.InvokeVoidAsync("StartLoading");
                order = new Order();
                order = Oldorder;
                Discount = (order.Discount == null) ? 0 : (double)order.Discount;
                OrderTax = (order.OrderTax == null) ? 0 : (double)order.OrderTax;
                Total = (order.Total == null) ? 0 : (double)order.Total;
                TotalItems = order.TotalItemCount;
                orderItems = new List<OrderItem>();
                orderItems.AddRange(order.OrderItem);
                UpdateTotals();
                await InvokeAsync(() => { StateHasChanged(); });
                await JSRuntime.InvokeVoidAsync("StopLoading");
            }
        }
        private async ValueTask<ItemsProviderResult<OrderItem>> LoadItems(ItemsProviderRequest request)
        {
            return new ItemsProviderResult<OrderItem>(orderItems, orderItems.Count);
        }

        void OnChangeQuantity(int value, int prodId)
        {
            var product = Products.FirstOrDefault(c => c.Id == prodId);
            AddOrderItem(product, value);
        }
        async void OnSubmit(Order model)
        {

        }
        void OnInvalidSubmit(Radzen.FormInvalidSubmitEventArgs args)
        {
            Log("InvalidSubmit", JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true }));
        }
        public async void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
            events.Add(DateTime.Now, "Dialog opened");
            await InvokeAsync(() => { StateHasChanged(); });
        }
        public async void Close(dynamic result)
        {

            events.Add(DateTime.Now, "Dialog closed. Result: " + result);
            if (result != null)
            {

                order = new Order();
                orderItems = new List<OrderItem>();
                UpdateTotals();
            }
            await InvokeAsync(() => { StateHasChanged(); });
        }
        public async void AddOrderItem(Product item, int Qyt)
        {
            if (orderItems == null)
            {
                orderItems = new List<OrderItem>();
            }
            OrderItem orderItem = new OrderItem();
            if (orderItems.Any(c => c.ProductId == item.Id))
            {
                orderItem = orderItems.FirstOrDefault(c => c.ProductId == item.Id);
                orderItems.Remove(orderItem);
                orderItem.Quantity = orderItem.Quantity + Qyt;
                orderItem.SubTotal = orderItem.Price * orderItem.Quantity;
                if (order.Id != 0)
                {
                    orderItem.Product = null;
                    orderItem.OrderId = order.Id;
                    if (orderItem.Id != 0)
                        await Http.PutAsJsonAsync<OrderItem>("/api/orderItems/Update", orderItem);
                    else
                        await Http.PostAsJsonAsync<OrderItem>("/api/orderItems/Insert", orderItem);
                }
                orderItem.Product =new Product { Id = item.Id, Name = item.Name };
                orderItems.Add(orderItem);
            }
            else
            {
                orderItem = new OrderItem
                {
                    Price = item.SalesPrice,
                    Product = new Product { Id = item.Id, Name = item.Name },
                    ProductId = item.Id,
                    Quantity = Qyt,
                };
                orderItem.SubTotal = orderItem.Price * orderItem.Quantity;
                if (order.Id != 0)
                {
                    orderItem.Product = null;
                    orderItem.OrderId = order.Id;
                    if (orderItem.Id != 0)
                        await Http.PutAsJsonAsync<OrderItem>("/api/orderItems/Update", orderItem);
                    else
                        await Http.PostAsJsonAsync<OrderItem>("/api/orderItems/Insert", orderItem);
                }
                orderItem.Product = new Product { Id = item.Id, Name = item.Name };
                orderItems.Add(orderItem);
            }

            TotalItems = orderItems.Sum(c => c.Quantity);
            Total = orderItems.Sum(c => c.SubTotal);
            TotalPayable = Total - Discount + (OrderTax * Total / 100);


            await InvokeAsync(() => { StateHasChanged(); });

        }
        async void DeleteItem(OrderItem item)
        {
            orderItems.Remove(item);
            TotalItems = orderItems.Sum(c => c.Quantity);
            Total = orderItems.Sum(c => c.SubTotal);
            TotalPayable = Total - Discount + (OrderTax * Total / 100);
            if (order.Id != 0)
            {
                var response = await Http.DeleteAsync("/api/orderItems/Delete/" + item.Id);
            }
        }
        void UpdateTotals()
        {
            TotalItems = orderItems.Sum(c => c.Quantity);
            Total = orderItems.Sum(c => c.SubTotal);
            TotalPayable = Total - Discount + (OrderTax * Total / 100);
        }
    }
}
