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
    public class ProductsController : _BaseController<Product, ProductDTO>
    {
        private readonly IProductService _productsService;

        public ProductsController(IProductService businessService) : base(businessService)
        {
            this._productsService = businessService;
        }
        [HttpGet, Route("GetDropDownListAll")]
        public virtual IActionResult GetDropDownListAll()
        {
            return Ok(this._BusinessService.GetAll<Product>().Select(x => new DropDownList { Id = x.Id, Name = x.Name }));
        }
    }
}
