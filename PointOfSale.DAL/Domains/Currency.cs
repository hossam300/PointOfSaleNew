using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class Currency
    {
        public int Id { get; set; }
        [Display(Name = "CurrencyName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
    }
}