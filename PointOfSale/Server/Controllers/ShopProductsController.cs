using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
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
    public class ShopProductsController : _BaseController<ShopProduct, ShopProduct>
    {
        private readonly IShopProductService _shopProductService;
        private readonly IUnitOfWork _unitOfWork;

        public ShopProductsController(IShopProductService businessService, IUnitOfWork unitOfWork) : base(businessService)
        {
            this._shopProductService = businessService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllByShopId/{id}")]
        public IActionResult GetAllByShopId(int id)
        {
            List<ShopProductDTO> ShopProducts = _shopProductService.GetAllByShopId(id);
            return Ok(ShopProducts);
        }
        [HttpPost("EditShopProduct")]
        public IActionResult EditShopProduct([FromBody] ShopProductDTO shopProduct)
        {
            var shopProducts = _shopProductService.GetAllWithoutInclude().Where(x => x.ShopId == shopProduct.ShopId && x.ProductId == shopProduct.ProductId).FirstOrDefault();
            if (shopProducts != null)
            {
                shopProducts.ActualQuantity = shopProduct.ActualQuantity;
                _shopProductService.Update(shopProducts);
                _unitOfWork.SaveChanges();
            }
            else
            {
                shopProducts = new ShopProduct
                {
                    ActualQuantity = shopProduct.ActualQuantity,
                    CreationDate = DateTime.Now,
                    CurrentQuantity = shopProduct.ActualQuantity,
                    OldQuantity = shopProduct.ActualQuantity,
                    ProductId = shopProduct.ProductId,
                    ShopId = shopProduct.ShopId
                };
                _shopProductService.Insert(shopProducts);
                _unitOfWork.SaveChanges();
            }
            return Ok(shopProducts);
        }
    }
}
