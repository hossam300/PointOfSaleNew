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
    public class OrderItemsController : _BaseController<OrderItem, OrderItem>
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService businessService) : base(businessService)
        {
            this._orderItemService = businessService;
        }
    }
}