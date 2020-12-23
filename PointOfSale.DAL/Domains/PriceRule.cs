using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class PriceRule
    {
        public int Id { get; set; }
        [Display(Name = "ApplyOn", ResourceType = typeof(Resources.Resources))]
        public virtual RuleApplyOn ApplyOn { get; set; }
    }
    public enum RuleApplyOn
    {
        [Display(Name = "AllProducts", ResourceType = typeof(Resources.Resources))]
        AllProducts = 1,
        [Display(Name = "ProductCategory", ResourceType = typeof(Resources.Resources))]
        ProductCategory = 2,
        [Display(Name = "Product", ResourceType = typeof(Resources.Resources))]
        Product = 3
    }
}