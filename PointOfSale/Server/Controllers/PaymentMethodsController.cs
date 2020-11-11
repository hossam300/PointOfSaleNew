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
    public class PaymentMethodsController : _BaseController<PaymentMethod, PaymentMethodDTO>
    {
        public PaymentMethodsController(IPaymentMethodService businessService) : base(businessService)
        {
        }
    }
}
