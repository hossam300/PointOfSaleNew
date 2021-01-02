using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Display(Name = "ImagePath", ResourceType = typeof(Resources.Resources))]
        public string ImagePath { get; set; }
        [Display(Name = "CategoryName", ResourceType = typeof(Resources.Resources))]
        public string CategoryName { get; set; }
        [Display(Name = "ParentCategory", ResourceType = typeof(Resources.Resources))]
        public int? ParentCategoryId { get; set; }
        public virtual ProductCategory ParentCategory { get; set; }
    }
}