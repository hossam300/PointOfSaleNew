using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class CustomerTax
    {
        public int Id { get; set; }
        [Display(Name = "TaxID", ResourceType = typeof(Resources.Resources))]
        public int TaxId { get; set; }
        public virtual Tax Tax { get; set; }
        [Display(Name = "Product", ResourceType = typeof(Resources.Resources))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}