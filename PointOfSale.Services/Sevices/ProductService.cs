using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSale.Services.Sevices
{
    public class ProductService : BusinessService<Product, ProductDTO>, IProductService
    {
        IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public void InsertAddProductDTO(AddProductDTO addProduct)
        {
            var product = new Product
            {
                Id = addProduct.Id,
                Name = addProduct.Name,
                ProductImage = addProduct.ProductImage,
                CanBeSold = addProduct.CanBeSold,
                CanBePurchased = addProduct.CanBePurchased,
                CanBeExpensed = addProduct.CanBeExpensed,
                CanBeRented = addProduct.CanBeRented,
                ProductType = addProduct.ProductType,
                ProductCategoryId = addProduct.ProductCategoryId,
                ProductCategory = GetProductCategory(addProduct.ProductCategory),
                InternalReference = addProduct.InternalReference,
                Barcode = addProduct.Barcode,
                SalesPrice = addProduct.SalesPrice,
                Cost = addProduct.Cost,
                CustomerTaxes = addProduct.CustomerTaxes.Select(x => new CustomerTax
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    TaxId = x.TaxId
                }).ToList(),
                CompanyId = addProduct.CompanyId,
                Notes = addProduct.Notes,
            };
            this.Insert(product);
            _UnitOfWork.SaveChanges();
        }

        public static ProductCategory GetProductCategory(ProductCategoryDTO productCategory)
        {
            return new ProductCategory
            {
                CategoryName = productCategory.CategoryName,
                Id = productCategory.Id,
                ImagePath = productCategory.ImagePath,
                ParentCategoryId = productCategory.ParentCategoryId
            };
        }

        public void UpdateAddProductDTO(AddProductDTO addProduct)
        {
            var Product = _unitOfWork.GetRepository<Product>().GetAllWithoutInclude().Include(x => x.CustomerTaxes)
               .Include(x => x.ProductCategory)
               .AsNoTracking().Where(c => c.Id == addProduct.Id).FirstOrDefault();
            _unitOfWork.GetRepository<CustomerTax>().Delete(Product.CustomerTaxes);
            _UnitOfWork.SaveChanges();
            Product.Id = addProduct.Id;
            Product.Name = addProduct.Name;
            Product.ProductImage = addProduct.ProductImage;
            Product.CanBeSold = addProduct.CanBeSold;
            Product.CanBePurchased = addProduct.CanBePurchased;
            Product.CanBeExpensed = addProduct.CanBeExpensed;
            Product.CanBeRented = addProduct.CanBeRented;
            Product.ProductType = addProduct.ProductType;
            Product.ProductCategoryId = addProduct.ProductCategoryId;
            Product.ProductCategory = GetProductCategory(addProduct.ProductCategory);
            Product.InternalReference = addProduct.InternalReference;
            Product.Barcode = addProduct.Barcode;
            Product.SalesPrice = addProduct.SalesPrice;
            Product.Cost = addProduct.Cost;
            Product.CustomerTaxes = addProduct.CustomerTaxes.Select(x => new CustomerTax
            {
                Id = x.Id,
                ProductId = x.ProductId,
                TaxId = x.TaxId
            }).ToList();
            Product.CompanyId = addProduct.CompanyId;
            Product.Notes = addProduct.Notes;
            _unitOfWork.GetRepository<Product>().Update(Product);
            _UnitOfWork.SaveChanges();
        }

        public void UpdateProductSalesDTO(ProductSalesDTO productSales)
        {
            var Product = _unitOfWork.GetRepository<Product>().GetAllWithoutInclude().Include(x => x.OptionalProducts)
              .Include(x => x.Category).Include(x => x.InvoicingPolicy)
              .AsNoTracking().Where(c => c.Id == productSales.Id).FirstOrDefault();
            _unitOfWork.GetRepository<OptionalProduct>().Delete(Product.OptionalProducts);
            _UnitOfWork.SaveChanges();
            Product.Id = productSales.Id;
            Product.InvoicingPolicy = productSales.InvoicingPolicy;
            Product.SubscriptionProduct = productSales.SubscriptionProduct;
            Product.IsEventTicket = productSales.IsEventTicket;
            Product.OptionalProducts = productSales.OptionalProducts.Select(x => new OptionalProduct { Id = x.Id, OptionalProductId = x.OptionalProductId, ProductId = x.ProductId }).ToList();
            Product.SalesDescription = productSales.SalesDescription;
            Product.AvailableInPOS = productSales.AvailableInPOS;
            Product.CategoryId = productSales.CategoryId;
            Product.Category = (Product.CategoryId != null) ? GetProductCategory(productSales.Category) : null;
            Product.ToWeighWithScale = productSales.ToWeighWithScale;
            _unitOfWork.GetRepository<Product>().Update(Product);
            _UnitOfWork.SaveChanges();
        }

        public void UpdateProductPOSDTO(ProductPOSDTO productPOSDTO)
        {
            var Product = _unitOfWork.GetRepository<Product>().GetAllWithoutInclude()
              .Include(x => x.Category).Include(x => x.InvoicingPolicy)
              .AsNoTracking().Where(c => c.Id == productPOSDTO.Id).FirstOrDefault();
            Product.Id = productPOSDTO.Id;
            Product.AvailableInPOS = productPOSDTO.AvailableInPOS;
            Product.CategoryId = productPOSDTO.CategoryId;
            Product.Category = (Product.CategoryId != null) ? GetProductCategory(productPOSDTO.Category) : null;
            Product.ToWeighWithScale = productPOSDTO.ToWeighWithScale;
            _unitOfWork.GetRepository<Product>().Update(Product);
            _UnitOfWork.SaveChanges();
        }

        public void UpdateProductPurchaseDTO(ProductPurchaseDTO productPurchase)
        {
            var Product = _unitOfWork.GetRepository<Product>().GetAllWithoutInclude().Include(x => x.VendorTaxes)
             .AsNoTracking().Where(c => c.Id == productPurchase.Id).FirstOrDefault();
            _unitOfWork.GetRepository<VendorTax>().Delete(Product.VendorTaxes);
            _UnitOfWork.SaveChanges();
            Product.Id = productPurchase.Id;
            Product.Procurement = productPurchase.Procurement;
            Product.ControlPolicy = productPurchase.ControlPolicy;
            Product.PurchaseDescription = productPurchase.PurchaseDescription;
            Product.VendorTaxes = productPurchase.VendorTaxes.Select(x => new VendorTax { Id = x.Id, TaxId = x.TaxId, ProductId = x.ProductId }).ToList();
            _unitOfWork.GetRepository<Product>().Update(Product);
            _UnitOfWork.SaveChanges();
        }

        public void UpdateProductInventoryDTO(ProductInventoryDTO productInventory)
        {
            var Product = _unitOfWork.GetRepository<Product>().GetAllWithoutInclude().AsNoTracking().Where(c => c.Id == productInventory.Id).FirstOrDefault();
            Product.Id = productInventory.Id;
            Product.Manufacture = productInventory.Manufacture;
            Product.Buy = productInventory.Buy;
            Product.CustomerLeadTime = productInventory.CustomerLeadTime;
            Product.DescriptionForDeliveryOrders = productInventory.DescriptionForDeliveryOrders;
            Product.DescriptionForReceipts = productInventory.DescriptionForReceipts;
            Product.HSCode = productInventory.HSCode;
            Product.ManufacturingLeadTime = productInventory.ManufacturingLeadTime;
            Product.Volume = productInventory.Volume;
            Product.Weight = productInventory.Weight;
            _unitOfWork.GetRepository<Product>().Update(Product);
            _UnitOfWork.SaveChanges();
        }
    }
}
