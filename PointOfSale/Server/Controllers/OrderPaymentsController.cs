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
    public class OrderPaymentsController : _BaseController<OrderPayment, OrderPayment>
    {
        private readonly IOrderPaymentService _orderPaymentService;

        public OrderPaymentsController(IOrderPaymentService businessService) : base(businessService)
        {
            this._orderPaymentService = businessService;
        }
        [HttpGet("GetAllGroubByMethod")]
        public IActionResult GetAllGroubByMethod()
        {
            var OrderPayment = _orderPaymentService.GetAll<OrderPayment>().GroupBy(x => x.PaymentMethod).Select(x => new PaymentMethodDTO
            {
                Id = x.Key.Id,
                MehtodName = x.Key.MehtodName,
                Cash = x.Sum(c => c.Amount),
                Orders = _orderPaymentService.GetOdersByMethod(x.Key.Id)
            });
            return Ok(OrderPayment);
        }
    }
}
