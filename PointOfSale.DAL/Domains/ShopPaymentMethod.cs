namespace PointOfSale.DAL.Domains
{
    public class ShopPaymentMethod
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}