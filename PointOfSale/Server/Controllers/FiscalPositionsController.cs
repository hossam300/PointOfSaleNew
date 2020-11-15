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
    public class FiscalPositionsController : _BaseController<FiscalPosition, FiscalPosition>
    {
        private readonly IFiscalPointOfSaleitionService FiscalPointOfSaleition;

        public FiscalPositionsController(IFiscalPointOfSaleitionService businessService) : base(businessService)
        {
            this.FiscalPointOfSaleition = businessService;
        }
        [HttpGet, Route("GetDropDownListAll")]
        public virtual IActionResult GetDropDownListAll()
        {
            return Ok(this._BusinessService.GetAll<FiscalPosition>().Select(x => new DropDownList { Id = x.Id, Name = x.FiscalPositionName }));
        }
    }
}
