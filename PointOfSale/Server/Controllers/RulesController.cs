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
    public class RulesController : _BaseController<Rules, Rules>
    {
        private readonly IRulesService _rulesService;

        public RulesController(IRulesService businessService) : base(businessService)
        {
            this._rulesService = businessService;
        }
    }
}
