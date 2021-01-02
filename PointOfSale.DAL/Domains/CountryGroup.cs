using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class CountryGroup
    {
        public int Id { get; set; }
        [Display(Name = "CountryGroupName", ResourceType = typeof(Resources.Resources))]
        public string CountryGroupName { get; set; }
    }
}