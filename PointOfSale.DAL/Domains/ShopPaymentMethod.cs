using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class ShopPaymentMethod
    {
        public int Id { get; set; }
        [Display(Name = "PaymentMethod", ResourceType = typeof(Resources.Resources))]
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}