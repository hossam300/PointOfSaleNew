using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PointOfSale.DAL.Domains;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Shops
{
    public partial class ShopProductList : ComponentBase
    {
        RadzenGrid<DAL.Domains.ShopProduct> ShopProductsGrid;
        IEnumerable<DAL.Domains.ShopProduct> ShopProducts;
        List<Product> Products = new List<Product>();
        List<Shop> Shops = new List<Shop>();
        IEnumerable<string> multipleValues = new string[] { };
        [Parameter] public int? id { get; set; }
        protected RadzenContent content1;

        protected RadzenTemplateForm<IEnumerable<DAL.Domains.ShopProduct>> formShopProduct;

        protected RadzenLabel label1;

        protected RadzenTextBox type;

        protected RadzenButton button1;

        protected RadzenButton button2;
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected JSRuntime JSRuntime { get; set; }
        [Inject]
        protected NavigationManager UriHelper { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("StartLoading");
            await Load();
            await JSRuntime.InvokeVoidAsync("StopLoading");

        }
        async Task Load()
        {
            if (id != null)
            {
                ShopProducts = await Http.GetFromJsonAsync<List<DAL.Domains.ShopProduct>>("/api/ShopProducts/GetByShopId/" + id);
                Products = await Http.GetFromJsonAsync<List<DAL.Domains.Product>>("/api/Products/GetAll/");
                Shops = await Http.GetFromJsonAsync<List<DAL.Domains.Shop>>("/api/Shops/GetAll/");
            }

        }
        void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            InvokeAsync(StateHasChanged);
        }
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        void Log(string eventName, string value)
        {
            events.Add(DateTime.Now, $"{eventName}: {value}");
        }
        void OnInvalidSubmit(Radzen.FormInvalidSubmitEventArgs args)
        {
            Log("InvalidSubmit", JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true }));
        }
        void EditRow(DAL.Domains.ShopProduct shopProduct)
        {
            ShopProductsGrid.EditRow(shopProduct);
        }

        void OnUpdateRow(DAL.Domains.ShopProduct order)
        {
            //  dbContext.Update(order);

            // For demo purposes only
            //order.Customer = dbContext.Customers.Find(order.CustomerID);
            //order.Employee = dbContext.Employees.Find(order.EmployeeID);

            // For production
            //dbContext.SaveChanges();
        }

        void SaveRow(DAL.Domains.ShopProduct shopProduct)
        {
            ShopProductsGrid.UpdateRow(shopProduct);
        }

        void CancelEdit(DAL.Domains.ShopProduct shopProduct)
        {
            ShopProductsGrid.CancelEditRow(shopProduct);

            // For production
            //var orderEntry = dbContext.Entry(order);
            //if (orderEntry.State == EntityState.Modified)
            //{
            //    orderEntry.CurrentValues.SetValues(orderEntry.OriginalValues);
            //    orderEntry.State = EntityState.Unchanged;
            //}
        }

        void DeleteRow(DAL.Domains.ShopProduct shopProduct)
        {
            if (ShopProducts.Contains(shopProduct))
            {
                //dbContext.Remove<Order>(order);

                //// For demo purposes only
                //orders.Remove(order);

                // For production
                //dbContext.SaveChanges();

                ShopProductsGrid.Reload();
            }
            else
            {
                ShopProductsGrid.CancelEditRow(shopProduct);
            }
        }

        void InsertRow()
        {
            ShopProductsGrid.InsertRow(new DAL.Domains.ShopProduct());
        }

        void OnCreateRow(DAL.Domains.ShopProduct shopProduct)
        {
            // dbContext.Add(order);

            // For demo purposes only
            //order.Customer = dbContext.Customers.Find(order.CustomerID);
            //order.Employee = dbContext.Employees.Find(order.EmployeeID);

            // For production
            //dbContext.SaveChanges();
        }
        public async void FormShopProductSubmit(IEnumerable<DAL.Domains.ShopProduct> model)
        {

            await JSRuntime.InvokeVoidAsync("StartLoading");
            foreach (var item in model)
            {
                bool formIsValid = true;
                if (formIsValid)
                {
                    await JSRuntime.InvokeVoidAsync("StopLoading");
                    return;
                }
                Log("Submit", JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true }));

                if (item.Id == 0)
                {
                    //// shop.AllowedEmployees = new List<ShopEmployee>();
                    // foreach (var item in multipleValues)
                    // {
                    //     shop.AllowedEmployees.Add(new ShopEmployee { UserId = item });
                    // }

                    using (var response = await Http.PostAsJsonAsync<DAL.Domains.ShopProduct>("/api/Shops/Insert", item))
                    {
                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<Shop>();

                        UriHelper.NavigateTo("/ShopInterface/" + data.Id);
                    }

                }
                else
                {
                    using (var response = await Http.PutAsJsonAsync<DAL.Domains.ShopProduct>("/api/Shops/Update", item))
                    {

                        // convert response data to JsonElement which can handle any JSON data
                        var data = await response.Content.ReadFromJsonAsync<DAL.Domains.ShopProduct>();

                        // get id property from JSON response data
                        //  var customerId = data[0].Id;
                        UriHelper.NavigateTo("/ShopInterface/" + data.Id);
                    }
                }
            }

            await JSRuntime.InvokeVoidAsync("StopLoading");
        }
        void btnCancelClick()
        {

        }
    }
}
