using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class Pricelist
    {
        public Pricelist()
        {
            AvailabeCountryGroups = new List<CountryGroup>();
            PriceRules = new List<PriceRule>();
        }
        public int Id { get; set; }
        [Display(Name = "PricelistName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "Currency", ResourceType = typeof(Resources.Resources))]
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        // TODO Add Addtional Fields
        public virtual List<PriceRule> PriceRules { get; set; }
        public virtual List<CountryGroup> AvailabeCountryGroups{ get; set; }
    }
}