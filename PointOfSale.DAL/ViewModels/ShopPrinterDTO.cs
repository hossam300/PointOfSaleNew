using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopPrinterDTO
    {
        public int Id { get; set; }
        [Display(Name = "Printer", ResourceType = typeof(Resources.Resources))]
        public int PrinterId { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
    }
}