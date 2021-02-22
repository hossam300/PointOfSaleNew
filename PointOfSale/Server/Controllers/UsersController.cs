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
    public class UsersController : _BaseController<SahlUserIdentity, SahlUserIdentity>
    {
        private readonly IUserService _userService;

        public UsersController(IUserService businessService) : base(businessService)
        {
            this._userService = businessService;
        }
        [HttpGet("GetDropDownListAll")]
        public IActionResult GetDropDownListAll()
        {
            return Ok(_userService.GetAll<SahlUserIdentity>().Select(x => new DropDownListString { Id = x.Id, Name = x.Name_AR }).ToList());
        }
    }
}