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
            return Ok(this._BusinessService.GetAll<Product>().Select(x => new DropDownList { Id = x.Id, Name = x.Name }).ToList());
        }
        //GetAddProductDTOById
        [HttpGet, Route("GetAddProductDTOById")]
        public virtual IActionResult GetAddProductDTOById()
        {
            return Ok(this._BusinessService.GetAll<Product>().Select(x => new AddProductDTO
            {
                Id = x.Id,
                Name = x.Name,
                Barcode = x.Barcode,
                CanBeExpensed = x.CanBeExpensed,
                CanBePurchased = x.CanBePurchased,
                CanBeRented = x.CanBeRented,
                CanBeSold = x.CanBeSold,
                CompanyId = x.CompanyId,
                Cost = x.Cost,
                CustomerTaxes = x.CustomerTaxes.Select(z => new CustomerTaxDTO { Id = z.Id, ProductId = z.ProductId, TaxId = z.TaxId, TaxName = z.Tax.Name }).ToList(),
                GenerateBarcode = x.GenerateBarcode,
                InternalReference = x.InternalReference,
                Notes = x.Notes,
                ProductCategoryId = x.ProductCategoryId,
                ProductImage = x.ProductImage,
                ProductType = x.ProductType,
                SalesPrice = x.SalesPrice,
                Company = (x.CompanyId != null) ? GetCompany(x.Company) : null,
                ProductCategory = (x.ProductCategoryId != null) ? GetProductCategory(x.ProductCategory) : null
            }).ToList());
        }
        [HttpPost("InsertAddProductDTO")]
        public IActionResult InsertAddProductDTO([FromBody] AddProductDTO addProduct)
        {
            return Ok(addProduct);
        }
        [HttpPost("InsertAddProductDTO")]
        public IActionResult UpdateAddProductDTO([FromBody] AddProductDTO addProduct)
        {
            return Ok(addProduct);
        }
        public static ProductCategoryDTO GetProductCategory(ProductCategory productCategory)
        {
            return new ProductCategoryDTO
            {

                CategoryName = productCategory.CategoryName,
                Id = productCategory.Id,
                ImagePath = productCategory.ImagePath,
                ParentCategoryId = productCategory.ParentCategoryId,
            };
        }

        public static CompanyDTO GetCompany(Company company)
        {
            return new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name
            };
        }
    }
}
