namespace PointOfSale.DAL.Domains
{
    public class ShopEmployee
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual SahlUserIdentity User { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}