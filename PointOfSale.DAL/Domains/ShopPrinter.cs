using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class ShopPrinter
    {
        public int Id { get; set; }
        [Display(Name = "Printer", ResourceType = typeof(Resources.Resources))]
        public int PrinterId { get; set; }
        public virtual Printer Printer { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }

    }
}