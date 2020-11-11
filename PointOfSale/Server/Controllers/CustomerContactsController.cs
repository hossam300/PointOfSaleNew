using Microsoft.AspNetCore.Hosting;
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
    public class CustomerContactsController : _BaseController<CustomerContact,CustomerContactDTO>
    {
        private ICustomerContactService _customerContactService;
        public CustomerContactsController(ICustomerContactService businessService) : base(businessService)
        {
            _customerContactService = businessService;
        }
        [HttpGet("GetCustomerContactBuCustomerId/{id}")]
        public IActionResult GetCustomerContactBuCustomerId(int id)
        {
            var customerContacts = _customerContactService.GetAll<CustomerContactDTO>().Where(c => c.CustomerId == id);
            return Ok(customerContacts);
        }
        [HttpGet("DisActive/{id}")]
        public IActionResult DisActive(int id)
        {
            var customer = _customerContactService.GetDetails(id);
            return Ok(true);
        }
    }
}
