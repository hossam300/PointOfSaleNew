using System.Collections.Generic;

namespace PointOfSale.DAL.Domains
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public int CountryGroupId { get; set; }
        public virtual CountryGroup CountryGroup { get; set; }
        
    }
}