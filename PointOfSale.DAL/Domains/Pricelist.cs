using System.Collections.Generic;

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
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        // TODO Add Addtional Fields
        public virtual List<PriceRule> PriceRules { get; set; }
        public virtual List<CountryGroup> AvailabeCountryGroups{ get; set; }
    }
}