using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class Country
    {
        public int Id { get; set; }
        [Display(Name = "CountryName", ResourceType = typeof(Resources.Resources))]
        public string CountryName { get; set; }
        [Display(Name = "CountryGroup", ResourceType = typeof(Resources.Resources))]
        public int CountryGroupId { get; set; }
        public virtual CountryGroup CountryGroup { get; set; }
        
    }
}