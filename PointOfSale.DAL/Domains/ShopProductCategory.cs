namespace PointOfSale.DAL.Domains
{
    public class ShopProductCategory
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}