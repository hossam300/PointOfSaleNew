using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopProductCategoryDTO
    {
        public int Id { get; set; }
        [Display(Name = "ProductCategory", ResourceType = typeof(Resources.Resources))]
        public int ProductCategoryId { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
    }
}