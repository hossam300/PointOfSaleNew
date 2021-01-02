using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class TaxMapping
    {
        // TODO Add Tax Adetional Prop
        public int Id { get; set; }
        [Display(Name = "TaxOnProduct", ResourceType = typeof(Resources.Resources))]
        public int? TaxOnProductId { get; set; }
        public virtual Tax TaxOnProduct { get; set; }
        [Display(Name = "TaxToApply", ResourceType = typeof(Resources.Resources))]
        public int? TaxToApplyId { get; set; }
        public virtual Tax TaxToApply { get; set; }
    }
}