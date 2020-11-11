using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Services.ISevices
{
    public interface IProductCategoriesService : IBusinessService<ProductCategory, ProductCategoryDTO>
    {
    }
}
