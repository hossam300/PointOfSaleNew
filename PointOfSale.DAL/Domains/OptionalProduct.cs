using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class OptionalProduct
    {
        public int Id { get; set; }
        [Display(Name = "Product", ResourceType = typeof(Resources.Resources))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Display(Name = "OptionalProducts", ResourceType = typeof(Resources.Resources))]
        public int OptionalProductId { get; set; }
        public virtual OptionalProduct OptionalProducts { get; set; }
    }
}