﻿using Microsoft.AspNetCore.Components;
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
        [Inject]
        protected NotificationService NotificationService { get; set; }

        List<Product> Products = new List<Product>();
        List<Product> ProductsData = new List<Product>();
        List<Product> ProductCats = new List<Product>();
        List<OrderItem> orderItems = new List<OrderItem>();
        List<Customer> customers = new List<Customer>();
        List<ProductCategory> Categories = new List<ProductCategory>();
        List<ProductCategory> ProductCategories = new List<ProductCategory>();
        Radzen.Blazor.RadzenLabel CustomerVaildation;
        Radzen.Blazor.RadzenLabel ItemsVaildation;
        Radzen.Blazor.RadzenAutoComplete BarcodeId;
        Radzen.Blazor.RadzenNumeric<double> Qty;
        Order order = new Order();
        double TotalItems = 0;
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
            ProductsData = Products;
            customers = await LoadCustomers();
            Categories = await Http.GetFromJsonAsync<List<ProductCategory>>("/api/ProductCategories/GetAll");
            ProductCats = GetProductsVyCategoryId(null);
            ProductCategories = GetProductCategories(0, 3, 0);
            //  BarcodeId.Value = "";
            DialogService.OnOpen += Open;
            DialogService.OnClose += Close;
            //     await BarcodeId.Element.FocusAsync();
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
            List<ProductCategory> categories = Categories.Skip(start * count).Take(count).ToList();
            ProductCategories = (categories == null) ? ProductCategories : categories;
            return ProductCategories;
        }
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        async Task<List<Customer>> LoadCustomers()
        {
            customers = await Http.GetFromJsonAsync<List<Customer>>("/api/Customers/GetAll");
            return customers;
        }
        async void HoldOrder()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "العميل مطلوب");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "اختار منتج على الاقل");
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
                order.OrderItem = new List<OrderItem>();
                order.OrderItem = orderItems;
                order.OrderType = OrderType.Draft;
                foreach (var item in order.OrderItem)
                {
                    item.Product.Category = null;
                    item.Product.ProductCategory = null;
                    item.Order = null;
                }
                var result = await DialogService.OpenAsync<PointOfSale.Client.Pages.Sessions.HoldOrder>("تعليق البيع", new Dictionary<string, object>() { { "Order", order } },
                      new Radzen.DialogOptions() { Width = "500px", Height = "325px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        async void CancelOrder()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "العميل مطلوب");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "اختار منتج على الاقل");
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
                var result = await DialogService.OpenAsync<CancelOrder>("الغاء الفاتورة", new Dictionary<string, object>() { { "Order", order } },
                        new Radzen.DialogOptions() { Width = "400px", Height = "250px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        async void PrintOrder()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "العميل مطلوب");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "اختار منتج على الاقل");
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
                var result = await DialogService.OpenAsync<PrintOrder>("طباعة الطلب", new Dictionary<string, object>() { { "Order", order } },
                        new Radzen.DialogOptions() { Width = "400px", Height = "500px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        async void PrintBill()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "العميل مطلوب");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "اختار منتج على الاقل");
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
                var result = await DialogService.OpenAsync<PrintBill>("طباعة الايصال", new Dictionary<string, object>() { { "Order", order } },
                     new Radzen.DialogOptions() { Width = "400px", Height = "500px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }
        }
        async void Payment()
        {
            if (order.CustomerId == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "العميل مطلوب");
            }
            else if (orderItems.Count == 0)
            {
                await JSRuntime.InvokeVoidAsync("requied", "اختار منتج على الاقل");
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
                var result = await DialogService.OpenAsync<Payment>("الدفع", new Dictionary<string, object>() { { "Order", order } },
                           new Radzen.DialogOptions() { Width = "600px", Height = "400px" });
                await InvokeAsync(() => { StateHasChanged(); });
            }

        }
        async void AddCustomer()
        {
            var result = await DialogService.OpenAsync<Customers.AddCustomer>(Loc["AddCustomer"], new Dictionary<string, object>() { { "Modal", true } },
                      new Radzen.DialogOptions() { Width = "80%", Height = "80%" });
            await InvokeAsync(() => { StateHasChanged(); });
        }
        void Log(string eventName, string value)
        {
            events.Add(DateTime.Now, $"{eventName}: {value}");
        }
        void OnChangeProduct(object value, string name)
        {
            if (value != null)
            {
                ProductsData = Products.Where(x => x.Barcode.Contains(value.ToString()) || x.Name.Contains(value.ToString())).ToList();
                var product = Products.FirstOrDefault(c => c.Barcode == value.ToString() || c.Name == value.ToString());
                AddOrderItem(product, 1);
                //  BarcodeId.Value = "";
            }
            else
            {
                ProductsData = Products;
            }

            Log($"{name} value changed to {value}", name);
        }
        //List<Product> OnLoadData(object value, string name)
        //{
        //    if (value != null)
        //    {
        //        ProductsData = Products.Where(c => c.Barcode.Contains(value.ToString()) || c.Name.Contains(value.ToString())).ToList();
        //    }
        //    else
        //    {
        //        ProductsData = Products;
        //    }
        //    return ProductsData;
        //    InvokeAsync(StateHasChanged);
        //}
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

       async void OnChangeQuantity(double value, int prodId)
        {
            var product = Products.FirstOrDefault(c => c.Id == prodId);
            AddOrderItemQunty(product, value);
            
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
                customers = await LoadCustomers();
                order = new Order();
                orderItems = new List<OrderItem>();
                UpdateTotals();
            }
            await InvokeAsync(() => { StateHasChanged(); });
        }
        public async void AddOrderItemQunty(Product item, double Qyt)
        {
            var shopproduct = item.ShopProducts.FirstOrDefault(x => x.ShopId == Id && x.ProductId == item.Id);
            if (shopproduct != null)
            {
                if (item != null)
                {
                    if (orderItems == null)
                    {
                        orderItems = new List<OrderItem>();
                    }
                    else
                    {
                        OrderItem orderItem = new OrderItem();
                        if (orderItems.Any(c => c.ProductId == item.Id))
                        {
                            if (Qyt> shopproduct.ActualQuantity)
                            {
                                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"لايوجد كمية متاحة");
                                
                                int index = orderItems.FindIndex(a => a.ProductId == item.Id);
                                orderItem = orderItems.FirstOrDefault(c => c.ProductId == item.Id);
                                orderItems.Remove(orderItem);
                                orderItem.Quantity = shopproduct.ActualQuantity;
                                orderItem.Price = orderItem.Price;
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
                                orderItem.Product = new Product { Id = item.Id, Name = item.Name, SalesPrice = item.SalesPrice };
                                orderItems.Insert(index, orderItem);
                            }
                            else
                            {
                                orderItem.Quantity = orderItem.Quantity;
                                int index = orderItems.FindIndex(a => a.ProductId == item.Id);
                                orderItem = orderItems.FirstOrDefault(c => c.ProductId == item.Id);
                                orderItems.Remove(orderItem);
                                orderItem.Price = orderItem.Price;
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
                                orderItem.Product = new Product { Id = item.Id, Name = item.Name, SalesPrice = item.SalesPrice };
                                orderItems.Insert(index, orderItem);
                            }
                           
                        }
                        else
                        {
                            if (Qyt > shopproduct.ActualQuantity)
                            {
                                orderItem = new OrderItem
                                {
                                    Price = item.SalesPrice,
                                    Product = new Product { Id = item.Id, Name = item.Name },
                                    ProductId = item.Id,
                                    Quantity = shopproduct.ActualQuantity,
                                };
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
                            }
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
                            orderItem.Product = new Product { Id = item.Id, Name = item.Name, SalesPrice = item.SalesPrice };
                            orderItems.Add(orderItem);
                        }

                        TotalItems = orderItems.Sum(c => c.Quantity);
                        Total = orderItems.Sum(c => c.SubTotal);
                        TotalPayable = Total - Discount + (OrderTax * Total / 100);
                    }
                    await InvokeAsync(() => { StateHasChanged(); });
                }
               
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"لايوجد كمية متاحة");
                return;
            }
          
        }
        public async void AddOrderItem(Product item, int Qyt)
        {
            var shopproduct = item.ShopProducts.FirstOrDefault(x => x.ShopId == Id && x.ProductId == item.Id);
            if (shopproduct != null)
            {
                if (item != null)
                {
                    if (orderItems == null)
                    {
                        orderItems = new List<OrderItem>();
                    }
                    else
                    {
                        OrderItem orderItem = new OrderItem();
                        if (orderItems.Any(c => c.ProductId == item.Id))
                        {
                            orderItem = orderItems.FirstOrDefault(c => c.ProductId == item.Id);
                            orderItems.Remove(orderItem);
                            orderItem.Price = orderItem.Price;
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
                            orderItem.Product = new Product { Id = item.Id, Name = item.Name, SalesPrice = item.SalesPrice };
                            orderItems.Add(orderItem);
                        }
                        else
                        {
                            orderItem = new OrderItem
                            {
                                Price = item.SalesPrice,
                                Product = new Product { Id = item.Id, Name = item.Name, SalesPrice = item.SalesPrice },
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
                            orderItem.Product = new Product { Id = item.Id, Name = item.Name, SalesPrice = item.SalesPrice };
                            orderItems.Add(orderItem);
                        }

                        TotalItems = orderItems.Sum(c => c.Quantity);
                        Total = orderItems.Sum(c => c.SubTotal);
                        TotalPayable = Total - Discount + (OrderTax * Total / 100);
                    }
                    await InvokeAsync(() => { StateHasChanged(); });
                }
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"لايوجد كمية متاحة");
            }
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
