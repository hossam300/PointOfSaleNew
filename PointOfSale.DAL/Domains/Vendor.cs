using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class Vendor
    {
        public int Id { get; set; }
        [Display(Name = "VendorName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
    }
}