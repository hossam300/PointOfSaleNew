using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class PurchaseOrderItem
    {
        public PurchaseOrderItem()
        {
            SubTotal = (Quantity * Price) - ProductDiscount;
        }
        public int Id { get; set; }
        [Display(Name = "PurchaseOrder", ResourceType = typeof(Resources.Resources))]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        [Display(Name = "Product", ResourceType = typeof(Resources.Resources))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resources.Resources))]
        public int Quantity { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resources.Resources))]
        public double Price { get; set; }
        [Display(Name = "SubTotal", ResourceType = typeof(Resources.Resources))]
        public double SubTotal { get; set; }
        [Display(Name = "ProductDiscount", ResourceType = typeof(Resources.Resources))]
        public double ProductDiscount { get; set; }
    }
}