namespace PointOfSale.DAL.ViewModels
{
    public class VendorProductDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
        public int VendorId { get; set; }
        public virtual VendorDTO Vendor { get; set; }
        public double Price { get; set; }
    }
}