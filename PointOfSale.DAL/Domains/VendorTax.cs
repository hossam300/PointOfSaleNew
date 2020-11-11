namespace PointOfSale.DAL.Domains
{
    public class VendorTax
    {
        public int Id { get; set; }
        public int TaxId { get; set; }
        public virtual Tax Tax { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}