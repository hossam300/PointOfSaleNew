using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class VendorProduct
    {
        public int Id { get; set; }
        [Display(Name = "Product", ResourceType = typeof(Resources.Resources))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Display(Name = "Vendor", ResourceType = typeof(Resources.Resources))]
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resources.Resources))]
        public double Price { get; set; }
    }
}