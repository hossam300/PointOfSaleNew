namespace PointOfSale.DAL.Domains
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual ProductCategory ParentCategory { get; set; }
    }
}