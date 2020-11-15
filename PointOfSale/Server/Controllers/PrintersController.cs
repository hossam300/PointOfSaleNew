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
    public class PrintersController : _BaseController<Printer, Printer>
    {
        private readonly IPrinterService Printer;

        public PrintersController(IPrinterService businessService) : base(businessService)
        {
            this.Printer = businessService;
        }
        [HttpGet, Route("GetDropDownListAll")]
        public virtual IActionResult GetDropDownListAll()
        {
            return Ok(this._BusinessService.GetAll<Printer>().Select(x => new DropDownList { Id = x.Id, Name = x.PrinterName }));
        }
    }
}
