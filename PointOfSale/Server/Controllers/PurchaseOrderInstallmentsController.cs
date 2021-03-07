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
    public class PurchaseOrderInstallmentsController : _BaseController<PurchaseOrderInstallment, PurchaseOrderInstallment>
    {
        private readonly IPurchaseOrderInstallmentService _purchaseOrderInstallmentService;

        public PurchaseOrderInstallmentsController(IPurchaseOrderInstallmentService businessService) : base(businessService)
        {
            this._purchaseOrderInstallmentService = businessService;
        }
    }
}