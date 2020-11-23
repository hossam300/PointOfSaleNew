using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace PointOfSale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : _BaseController<Order, Order>
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService businessService) : base(businessService)
        {
            this._orderService = businessService;
        }
        [HttpGet("GetOrderByNo/{id}")]
        public async Task<IActionResult> GetOrderByNo(string id)
        {

            var order = await _orderService.GetOrderByNo(id);
            if (order==null)
            {
                order = new Order();
            }
            return Ok(order);
        }
    }
}