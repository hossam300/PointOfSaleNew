using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class ShopProductCategory
    {
        public int Id { get; set; }
        [Display(Name = "ProductCategory", ResourceType = typeof(Resources.Resources))]
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}