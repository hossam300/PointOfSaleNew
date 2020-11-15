namespace PointOfSale.DAL.Domains
{
    public class ShopPricelist
    {
        public int Id { get; set; }
        public int PricelistId { get; set; }
        public virtual Pricelist Pricelist { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}