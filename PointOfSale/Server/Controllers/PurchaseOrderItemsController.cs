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
    public class PurchaseOrderItemsController : _BaseController<PurchaseOrderItem, PurchaseOrderItem>
    {
        private readonly IPurchaseOrderItemService _orderItemService;

        public PurchaseOrderItemsController(IPurchaseOrderItemService businessService) : base(businessService)
        {
            this._orderItemService = businessService;
        }
        [HttpGet("GetChartGroubByProductCategoryOnPeriod")]
        public IActionResult GetChartGroubByProductCategoryOnPeriod(DateTime? StartDate, DateTime? EndDate)
        {
            var OrderPayment = _orderItemService.GetAllWithInclude().Where(c => (c.PurchaseOrder.OrderDate >= StartDate || StartDate == null) && (c.PurchaseOrder.OrderDate <= EndDate || EndDate == null))
                .GroupBy(x => x.Product.ProductCategory).Select(x => new PiChartsDTO
                {
                    Text = x.Key.CategoryName,
                    Value = x.Sum(a => a.SubTotal)
                });
            return Ok(OrderPayment);
        }
    }
}