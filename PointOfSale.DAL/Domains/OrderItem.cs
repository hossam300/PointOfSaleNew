using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class OrderItem
    {
        public OrderItem()
        {
            SubTotal = (Quantity * Price) - ProductDiscount;
        }
        public int Id { get; set; }
        [Display(Name = "Order", ResourceType = typeof(Resources.Resources))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
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