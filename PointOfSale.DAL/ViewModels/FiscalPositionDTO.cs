using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.ViewModels
{
    public class FiscalPositionDTO
    {
        public int Id { get; set; }
        [Display(Name = "FiscalPositionName", ResourceType = typeof(Resources.Resources))]
        public string FiscalPositionName { get; set; }
        [Display(Name = "DetectAutomatically", ResourceType = typeof(Resources.Resources))]
        public bool DetectAutomatically { get; set; }
        [Display(Name = "VATRequired", ResourceType = typeof(Resources.Resources))]
        public bool VATRequired { get; set; }
        [Display(Name = "CountryGroup", ResourceType = typeof(Resources.Resources))]
        public int? CountryGroupId { get; set; }
        [Display(Name = "Country", ResourceType = typeof(Resources.Resources))]
        public int? CountryId { get; set; }
    }
}