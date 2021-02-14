using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopPaymentMethodDTO
    {
        public int Id { get; set; }
        [Display(Name = "PaymentMethod", ResourceType = typeof(Resources.Resources))]
        public int PaymentMethodId { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
    }
}