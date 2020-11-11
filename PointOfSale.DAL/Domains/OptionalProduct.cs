namespace PointOfSale.DAL.Domains
{
    public class OptionalProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OptionalProductId { get; set; }
        public virtual OptionalProduct OptionalProducts { get; set; }
    }
}