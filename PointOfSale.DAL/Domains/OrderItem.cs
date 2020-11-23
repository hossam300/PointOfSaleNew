namespace PointOfSale.DAL.Domains
{
    public class OrderItem
    {
        public OrderItem()
        {
            SubTotal = (Quantity * Price) - ProductDiscount;
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public double ProductDiscount { get; set; }
    }
}