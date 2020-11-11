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
    public class LoyaltyProgramsController : _BaseController<LoyaltyProgram, LoyaltyProgram>
    {
        private readonly ILoyaltyProgramService _loyaltyProgramService;

        public LoyaltyProgramsController(ILoyaltyProgramService businessService) : base(businessService)
        {
            this._loyaltyProgramService = businessService;
        }
    }
}