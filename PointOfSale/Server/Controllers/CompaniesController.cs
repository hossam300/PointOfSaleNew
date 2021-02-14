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
    public class CompaniesController : _BaseController<Company, CompanyDTO>
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService businessService) : base(businessService)
        {
            this._companyService = businessService;
        }
        [HttpGet("GetAllCompanyDTO")]
        public IActionResult GetAllCompanyDTO()
        {
            var CompanyDTOs = _companyService.GetAllWithoutInclude().Select(x => new CompanyDTO { Id = x.Id, Name = x.Name }).ToList();
            return Ok(CompanyDTOs);
        }
    }
}
