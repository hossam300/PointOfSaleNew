using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class BarcodeScanner
    {
        public int Id { get; set; }
        [Display(Name = "BarcodeNomenclature", ResourceType = typeof(Resources.Resources))]
        public string BarcodeNomenclature { get; set; }
        [Display(Name = "Sequence", ResourceType = typeof(Resources.Resources))]
        public string Sequence { get; set; }
        [Display(Name = "Encoding", ResourceType = typeof(Resources.Resources))]
        public string Encoding { get; set; }
        [Display(Name = "BarcodePattern", ResourceType = typeof(Resources.Resources))]
        public string BarcodePattern { get; set; }
        [Display(Name = "BarcodeType", ResourceType = typeof(Resources.Resources))]
        public int? BarcodeTypeId { get; set; }
        public virtual BarcodeType BarcodeType { get; set; }
    }
}