using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class ShopPricelist
    {
        public int Id { get; set; }
        [Display(Name = "Pricelist", ResourceType = typeof(Resources.Resources))]
        public int PricelistId { get; set; }
        public virtual Pricelist Pricelist { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}