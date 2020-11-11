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
    public class VendorProductsController : _BaseController<VendorProduct, VendorProduct>
    {
        private readonly IVendorProductService _vendorProductSService;

        public VendorProductsController(IVendorProductService businessService) : base(businessService)
        {
            this._vendorProductSService = businessService;
        }
        [HttpGet("GetAllByProductId/{id}")]
        public IActionResult GetCustomerContactBuCustomerId(int id)
        {
            var customerContacts = _vendorProductSService.GetAll<VendorProduct>().Where(c => c.ProductId == id);
            return Ok(customerContacts);
        }
    }
}
