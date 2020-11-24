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
    public class SessionsController : _BaseController<Session, Session>
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService businessService) : base(businessService)
        {
            this._sessionService = businessService;
        }
        [HttpGet("GetByShopId/{id}")]
        public IActionResult GetByShopId(int id)
        {
            var session = _sessionService.GetAll<Session>().FirstOrDefault(c => c.ShopId == id&&c.Status==Status.Open);
            return Ok(session ?? new Session());
        }
        [HttpGet("GetAllByShopId/{id}")]
        public IActionResult GetAllByShopId(int id)
        {
            var session = _sessionService.GetAll<Session>().Where(c => c.ShopId == id ).ToList();
            return Ok(session);
        }
    }
}