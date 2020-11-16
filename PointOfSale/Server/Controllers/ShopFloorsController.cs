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
    public class ShopFloorsController : _BaseController<ShopFloor, ShopFloor>
    {
        private readonly IShopFloorService _shopFloorService;

        public ShopFloorsController(IShopFloorService businessService) : base(businessService)
        {
            this._shopFloorService = businessService;
        }
    }
}