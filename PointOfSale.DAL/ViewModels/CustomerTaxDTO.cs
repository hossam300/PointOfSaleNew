using PointOfSale.DAL.Domains;

namespace PointOfSale.DAL.ViewModels
{
    public class CustomerTaxDTO
    {
        public int Id { get; set; }
        public int TaxId { get; set; }
        public int ProductId { get; set; }
        public string TaxName { get; set; }
    }
}