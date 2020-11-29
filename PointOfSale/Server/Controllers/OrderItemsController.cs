using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : _BaseController<OrderItem, OrderItem>
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService businessService) : base(businessService)
        {
            this._orderItemService = businessService;
        }
        [HttpGet("GetChartGroubByProductCategoryOnPeriod")]
        public IActionResult GetChartGroubByProductCategoryOnPeriod(DateTime? StartDate, DateTime? EndDate)
        {
            var OrderPayment = _orderItemService.GetAllWithInclude().Where(c => (c.Order.OrderDate >= StartDate || StartDate == null) && (c.Order.OrderDate <= EndDate || EndDate == null))
                .GroupBy(x => x.Product.ProductCategory).Select(x => new PiChartsDTO
                {
                    Text = x.Key.CategoryName,
                    Value = x.Sum(a => a.SubTotal)
                });
            return Ok(OrderPayment);
        }
    }
}