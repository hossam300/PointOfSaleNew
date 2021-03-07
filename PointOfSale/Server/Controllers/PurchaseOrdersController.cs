using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : _BaseController<PurchaseOrder, PurchaseOrder>
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrdersController(IPurchaseOrderService businessService) : base(businessService)
        {
            this._purchaseOrderService = businessService;
        }
        [HttpGet("GetAllWithInclude")]

        public IActionResult GetAllWithInclude()
        {
            return Ok(_purchaseOrderService.GetAllWithInclude());
        }
        [HttpPost("InsertPurchaseOrders")]
        public IActionResult InsertPurchaseOrders([FromBody] PurchaseOrder purchaseOrder)
        {
            _purchaseOrderService.InsertPurchaseOrder(purchaseOrder);

            return Ok(purchaseOrder);
        }
        [HttpGet("GetPurchaseOrderByNo/{id}")]
        public async Task<IActionResult> GetPurchaseOrderByNo(string id)
        {

            var order = await _purchaseOrderService.GetOrderByNo(id);
            if (order == null)
            {
                order = new PurchaseOrder();
            }
            return Ok(order);
        }
        [HttpGet("GetAllWithIncludeByShopId/{id}")]

        public IActionResult GetAllWithIncludeByShopId(int id)
        {
            return Ok(_purchaseOrderService.GetAllWithInclude().Where(c => c.ShopId == id).ToList());
        }
        [HttpGet("GetAllWithIncludeToday")]
        public IActionResult GetAllWithIncludeToday()
        {
            return Ok(_purchaseOrderService.GetAllWithInclude().Where(c => c.OrderDate.Date == DateTime.Now.Date).ToList());
        }
        [HttpGet("GetOrderNumber")]
        public IActionResult GetOrderNumber()
        {
            var orderNum = (_purchaseOrderService.GetAllWithInclude().Max(x => x.Id) + 1);
            return Ok(orderNum);
        }

        public IActionResult GetAllWithIncludeByCustomerId(int id)
        {
            return Ok(_purchaseOrderService.GetAllWithInclude().Where(c => c.SupplierId == id).ToList());
        }
        [HttpGet("GetOrderByNo/{id}")]
        public async Task<IActionResult> GetOrderByNo(string id)
        {

            var order = await _purchaseOrderService.GetOrderByNo(id);
            if (order == null)
            {
                order = new PurchaseOrder();
            }
            return Ok(order);
        }
        [HttpGet("GetAllOrderOnPeriod")]
        public IActionResult GetAllOrderOnPeriod(DateTime? StartDate, DateTime? EndDate)
        {
            return Ok(_purchaseOrderService.GetAllWithInclude().Where(c => (c.OrderDate >= StartDate || StartDate == null) && (c.OrderDate <= EndDate || EndDate == null)).ToList());
        }
    }
}