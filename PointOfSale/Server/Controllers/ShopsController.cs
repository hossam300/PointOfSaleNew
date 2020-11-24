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
            return Ok(new ShopDTO { Id = shop.Id, ShopName = shop.Name, Footer = shop.Footer, Header = shop.Header, Branch = null });
        }
        [HttpGet("GetAllWithoutInclude")]
        public IActionResult GetAllWithoutInclude()
        {
            var shop = _shopService.GetAllWithoutInclude().Select(c => new ShopDTO
            {
                Id = c.Id,
                ShopName = c.Name,
                Footer = c.Footer,
                Header = c.Header,
                Branch = null,
                session = _shopService.GetAllSessionByShopId(c.Id).Select(x => new SessionDTO
                {
                    Id = x.Id,
                    SessionNo = x.SessionNo,
                    Status = x.Status,
                    ClosedDate = x.ClosedDate,
                    Cash = _shopService.GetOrderCashBySessionId(x.Id),
                    CosedById = x.CosedById,
                    CreationDate = x.CreationDate,
                    CreatorId = x.CreatorId,
                    ShopId = x.ShopId
                }).OrderByDescending(x => x.Id).FirstOrDefault()
            });
            return Ok(shop);
        }
        BranchDTO GetBranch(Branch branch)
        {
            return new BranchDTO { Id = branch.Id, Name = branch.Name };
        }
    }
}