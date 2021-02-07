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
    public class SupplierContactsController : _BaseController<SupplierContact, SupplierContact>
    {
        private ISupplierContactService _supplierContactService;
        public SupplierContactsController(ISupplierContactService businessService) : base(businessService)
        {
            _supplierContactService = businessService;
        }
        [HttpGet("GetSupplierContactBuSupplierId/{id}")]
        public IActionResult GetSupplierContactBuSupplierId(int id)
        {
            var SupplierContacts = _supplierContactService.GetAll<SupplierContact>().Where(c => c.SupplierId == id);
            return Ok(SupplierContacts);
        }
        [HttpGet("DisActive/{id}")]
        public IActionResult DisActive(int id)
        {
            var Supplier = _supplierContactService.GetDetails(id);
            return Ok(true);
        }
    }
}
