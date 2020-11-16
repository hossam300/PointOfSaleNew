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
    public class ShopProductCategoriesController : _BaseController<ShopProductCategory, ShopProductCategory>
    {
        private readonly IShopProductCategoryService _ShopProductCategoryService;

        public ShopProductCategoriesController(IShopProductCategoryService businessService) : base(businessService)
        {
            this._ShopProductCategoryService = businessService;
        }
    }
}