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
    public class VendorsController : _BaseController<Vendor, Vendor>
    {
        private readonly IVendorService _LoyaltyProgramService;

        public VendorsController(IVendorService businessService) : base(businessService)
        {
            this._LoyaltyProgramService = businessService;
        }
    }
}
