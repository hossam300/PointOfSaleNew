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
    public class ProductCategoriesController : _BaseController<ProductCategory, ProductCategoryDTO>
    {
        private readonly IProductCategoriesService _productCategoriesService;

        public ProductCategoriesController(IProductCategoriesService businessService) : base(businessService)
        {
            this._productCategoriesService = businessService;
        }
        [HttpGet, Route("GetDropDownListAll")]
        public virtual IActionResult GetDropDownListAll()
        {
            return Ok(this._BusinessService.GetAll<ProductCategory>().Select(x => new DropDownList { Id = x.Id, Name = x.CategoryName }));
        }
        [HttpGet("GetAllProductCategoryDTO")]
        public virtual IActionResult GetAllProductCategoryDTO()
        {
            return Ok(this._BusinessService.GetAll<ProductCategory>().Select(x => new ProductCategoryDTO { Id = x.Id, CategoryName = x.CategoryName, ImagePath = x.ImagePath, ParentCategoryId = x.ParentCategoryId }).ToList());
        }
    }
}
