namespace PointOfSale.DAL.Domains
{
    public class ShopPrinter
    {
        public int Id { get; set; }
        public int PrinterId { get; set; }
        public virtual Printer Printer { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }

    }
}