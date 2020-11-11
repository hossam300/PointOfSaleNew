namespace PointOfSale.DAL.Domains
{
    public class VendorProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public double Price { get; set; }
    }
}