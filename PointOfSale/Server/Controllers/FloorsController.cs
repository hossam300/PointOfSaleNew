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
    public class FloorsController : _BaseController<Floor, Floor>
    {
        private readonly IFloorService _floorService;

        public FloorsController(IFloorService businessService) : base(businessService)
        {
            this._floorService = businessService;
        }
        [HttpGet, Route("GetDropDownListAll")]
        public virtual IActionResult GetDropDownListAll()
        {
            return Ok(this._BusinessService.GetAll<Floor>().Select(x => new DropDownList { Id = x.Id, Name = x.FloorName }));
        }

    }
}
