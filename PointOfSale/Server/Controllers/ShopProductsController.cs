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
    public class ShopProductsController : _BaseController<ShopProduct, ShopProduct>
    {
        private readonly IShopProductService _shopProductService;

        public ShopProductsController(IShopProductService businessService) : base(businessService)
        {
            this._shopProductService = businessService;
        }
    }
}
