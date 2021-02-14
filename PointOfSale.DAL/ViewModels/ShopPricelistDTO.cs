using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopPricelistDTO
    {
        public int Id { get; set; }
        [Display(Name = "Pricelist", ResourceType = typeof(Resources.Resources))]
        public int PricelistId { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
    }
}