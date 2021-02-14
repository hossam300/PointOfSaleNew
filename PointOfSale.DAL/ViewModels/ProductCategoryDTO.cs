using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ProductCategoryDTO
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual ProductCategoryDTO ParentCategory { get; set; }
    }
}
