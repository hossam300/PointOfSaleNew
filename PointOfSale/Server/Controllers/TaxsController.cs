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
    public class TaxsController : _BaseController<Tax, Tax>
    {
        private readonly ITaxService _companyService;

        public TaxsController(ITaxService businessService) : base(businessService)
        {
            this._companyService = businessService;
        }
        [HttpGet("GetAllTaxDTO")]
        public IActionResult GetAllTaxDTO()
        {
            var CompanyDTOs = _companyService.GetAllWithoutInclude().Select(x => new TaxDTO { Id = x.Id, Name = x.Name }).ToList();
            return Ok(CompanyDTOs);
        }
    }
}