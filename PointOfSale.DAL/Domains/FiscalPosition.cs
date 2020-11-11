using System.Collections.Generic;

namespace PointOfSale.DAL.Domains
{
    public class FiscalPointOfSaleition
    {
        public int Id { get; set; }
        public int FiscalPointOfSaleitionName { get; set; }
        public bool DetectAutomatically { get; set; }
        public bool VATRequired { get; set; }
        public int? CountryGroupId { get; set; }
        public virtual CountryGroup CountryGroup { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        #region Tax Mapping 
        public virtual List<TaxMapping> TaxMappings { get; set; }
        #endregion
        #region Account Mapping 
        #endregion
    }
}