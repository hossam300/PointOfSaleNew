using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class OrderPayment
    {
        public int Id { get; set; }
        [Display(Name = "Order", ResourceType = typeof(Resources.Resources))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Display(Name = "PaymentMethod", ResourceType = typeof(Resources.Resources))]
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Amount", ResourceType = typeof(Resources.Resources))]
        public double Amount { get; set; }
    }
}