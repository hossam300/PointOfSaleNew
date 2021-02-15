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
        [HttpGet("GetProductSalesDTOById/{id}")]
        public IActionResult GetProductSalesDTOById(int id)
        {
            return Ok(_productsService.GetAll<Product>().Select(x => new ProductSalesDTO
            {
                Id = x.Id,
                AvailableInPOS = x.AvailableInPOS,
                CategoryId = x.CategoryId,
                InvoicingPolicy = x.InvoicingPolicy,
                Category = (x.CategoryId != null) ? GetProductCategory(x.Category) : null,
                IsEventTicket = x.IsEventTicket,
                OptionalProducts = x.OptionalProducts.Select(z => new OptionalProductDTO { Id = z.Id, OptionalProductId = z.OptionalProductId, ProductId = z.ProductId }).ToList(),
                SalesDescription = x.SalesDescription,
                SubscriptionProduct = x.SubscriptionProduct,
                ToWeighWithScale = x.ToWeighWithScale
            }).FirstOrDefault(x => x.Id == id));
        }
        [HttpGet("GetProductPOSDTOById/{id}")]
        public IActionResult GetProductPOSDTOById(int id)
        {
            return Ok(_productsService.GetAll<Product>().Select(x => new ProductPOSDTO
            {
                Id = x.Id,
                AvailableInPOS = x.AvailableInPOS,
                CategoryId = x.CategoryId,
                Category = (x.CategoryId != null) ? GetProductCategory(x.Category) : null,
                ToWeighWithScale = x.ToWeighWithScale,

            }).FirstOrDefault(x => x.Id == id));
        }

        public static ProductCategoryDTO GetProductCategory(ProductCategory category)
        {
            return new ProductCategoryDTO
            {
                CategoryName = category.CategoryName,
                Id = category.Id,
                ImagePath = category.ImagePath,
                ParentCategoryId = category.ParentCategoryId
            };
        }
        [HttpGet("GetProductPurchaseDTOById/{id}")]
        public IActionResult GetProductPurchaseDTOById(int id)
        {
            return Ok(_productsService.GetAll<Product>().Select(x => new ProductPurchaseDTO
            {
                Id = x.Id,
                ControlPolicy = x.ControlPolicy,
                Procurement = x.Procurement,
                PurchaseDescription = x.PurchaseDescription,
                VendorTaxes = x.VendorTaxes.Select(z => new VendorTaxDTO { Id = z.Id, ProductId = z.ProductId, TaxId = z.TaxId, TaxName = z.Tax.Name }).ToList()
            }).FirstOrDefault(x => x.Id == id));
        }
        [HttpGet("GetProductInventoryDTOById/{id}")]
        public IActionResult GetProductInventoryDTOById(int id)
        {
            return Ok(_productsService.GetAll<Product>().Select(x => new ProductInventoryDTO
            {
                Id = x.Id,
                Manufacture = x.Manufacture,
                Buy = x.Buy,
                CustomerLeadTime = x.CustomerLeadTime,
                DescriptionForDeliveryOrders = x.DescriptionForDeliveryOrders,
                DescriptionForReceipts = x.DescriptionForReceipts,
                HSCode = x.HSCode,
                ManufacturingLeadTime = x.ManufacturingLeadTime,
                Volume = x.Volume,
                Weight = x.Weight
            }).FirstOrDefault(x => x.Id == id));
        }
        [HttpGet, Route("GetDropDownListAll")]
        public virtual IActionResult GetDropDownListAll()
        {
            return Ok(_productsService.GetAll<Product>().Select(x => new DropDownList { Id = x.Id, Name = x.Name }).ToList());
        }
        //GetAddProductDTOById
        [HttpGet, Route("GetAddProductDTOById/{id}")]
        public virtual IActionResult GetAddProductDTOById(int id)
        {
            return Ok(_productsService.GetAll<Product>().Select(x => new AddProductDTO
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
                Company = GetCompany(x.Company),
                ProductCategory = (x.ProductCategoryId != null) ? GetProductCategoryDTO(x.ProductCategory) : null
            }).FirstOrDefault(x => x.Id == id));
        }
        [HttpPost("InsertAddProductDTO")]
        public IActionResult InsertAddProductDTO([FromBody] AddProductDTO addProduct)
        {
            _productsService.InsertAddProductDTO(addProduct);
            return Ok(addProduct);
        }
        [HttpPost("UpdateAddProductDTO")]
        public IActionResult UpdateAddProductDTO([FromBody] AddProductDTO addProduct)
        {
            _productsService.UpdateAddProductDTO(addProduct);
            return Ok(addProduct);
        }
        [HttpPost("UpdateProductPurchaseDTO")]
        public IActionResult UpdateProductPurchaseDTO([FromBody] ProductPurchaseDTO productPurchase)
        {
            _productsService.UpdateProductPurchaseDTO(productPurchase);
            return Ok(productPurchase);
        }
        [HttpPost("UpdateProductPOSDTO")]
        public IActionResult UpdateProductPOSDTO([FromBody] ProductPOSDTO productPOSDTO)
        {
            _productsService.UpdateProductPOSDTO(productPOSDTO);
            return Ok(productPOSDTO);
        }

        [HttpPost("UpdateProductSalesDTO")]
        public IActionResult UpdateProductSalesDTO([FromBody] ProductSalesDTO productSales)
        {
            _productsService.UpdateProductSalesDTO(productSales);
            return Ok(productSales);
        }
        [HttpPost("UpdateProductInventoryDTO")]
        public IActionResult UpdateProductInventoryDTO([FromBody] ProductInventoryDTO productInventory)
        {
            _productsService.UpdateProductInventoryDTO(productInventory);
            return Ok(productInventory);
        }
        public static ProductCategoryDTO GetProductCategoryDTO(ProductCategory productCategory)
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
