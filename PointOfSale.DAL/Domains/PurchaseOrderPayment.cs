using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class PurchaseOrderPayment
    {
        public int Id { get; set; }
        [Display(Name = "PurchaseOrder", ResourceType = typeof(Resources.Resources))]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        [Display(Name = "PaymentMethod", ResourceType = typeof(Resources.Resources))]
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Amount", ResourceType = typeof(Resources.Resources))]
        public double Amount { get; set; }
    }
}