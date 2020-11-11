namespace PointOfSale.DAL.Domains
{
    public class PriceRule
    {
        public int Id { get; set; }
        public virtual RuleApplyOn ApplyOn { get; set; }
    }
    public enum RuleApplyOn
    {
        AllProducts = 1,
        ProductCategory = 2,
        Product = 3
    }
}