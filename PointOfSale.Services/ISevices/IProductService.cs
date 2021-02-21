using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Services.ISevices
{
    public interface IProductService : IBusinessService<Product, ProductDTO>
    {
        AddProductDTO InsertAddProductDTO(AddProductDTO addProduct);
        void UpdateAddProductDTO(AddProductDTO addProduct);
        void UpdateProductSalesDTO(ProductSalesDTO productSales);
        void UpdateProductPOSDTO(ProductPOSDTO productPOSDTO);
        void UpdateProductPurchaseDTO(ProductPurchaseDTO productPurchase);
        void UpdateProductInventoryDTO(ProductInventoryDTO productInventory);
        AddProductDTO GetAddProductDTOById(int id);
    }
}
