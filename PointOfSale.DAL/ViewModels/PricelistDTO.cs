using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.ViewModels
{
    public class PricelistDTO
    {
        public int Id { get; set; }
        [Display(Name = "PricelistName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "Currency", ResourceType = typeof(Resources.Resources))]
        public int CurrencyId { get; set; }
    }
}