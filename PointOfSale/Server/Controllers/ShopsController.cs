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
    public class ShopsController : _BaseController<Shop, Shop>
    {
        private readonly IShopService _shopService;

        public ShopsController(IShopService businessService) : base(businessService)
        {
            this._shopService = businessService;
        }
        [HttpGet("GetByIdWithoutInclude/{id}")]
        public IActionResult GetByIdWithoutInclude(int id)
        {
            var shop = _shopService.GetDetails(id);
            return Ok(new ShopDTO { Id = shop.Id, ShopName = shop.Name, Footer = shop.Footer, Header = shop.Header,Branch=null });
        }
        [HttpGet("GetAllWithoutInclude")]
        public IActionResult GetAllWithoutInclude()
        {
            var shop = _shopService.GetAllWithoutInclude().Select(c => new ShopDTO { Id = c.Id, ShopName = c.Name, Footer = c.Footer, Header = c.Header ,Branch=null}).ToList();
            return Ok(shop);
        }
        BranchDTo GetBranch(Branch branch)
        {
            return new BranchDTo { Id = branch.Id, Name = branch.Name };
        }
    }
}