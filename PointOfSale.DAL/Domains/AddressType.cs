using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class AddressType
    {
        public int Id { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Resources.Resources))]
        public string Type { get; set; }
    }
}