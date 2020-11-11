namespace PointOfSale.DAL.Domains
{
    public class BarcodeScanner
    {
        public int Id { get; set; }
        public string BarcodeNomenclature { get; set; }
        public string Sequence { get; set; }
        public string Encoding { get; set; }
        public string BarcodePattern { get; set; }
        public int? BarcodeTypeId { get; set; }
        public virtual BarcodeType BarcodeType { get; set; }
    }
}