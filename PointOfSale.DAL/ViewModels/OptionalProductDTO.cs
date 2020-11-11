namespace PointOfSale.DAL.ViewModels
{
    public class OptionalProductDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int OptionalProductId { get; set; }
        public ProductDTO OptionalProducts { get; set; }
    }
}