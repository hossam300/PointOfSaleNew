namespace PointOfSale.DAL.Domains
{
    public class OrderPayment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public double Amount { get; set; }
    }
}