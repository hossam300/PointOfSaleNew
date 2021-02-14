using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopFloorDTO
    {
        public int Id { get; set; }
        [Display(Name = "Floor", ResourceType = typeof(Resources.Resources))]
        public int FloorId { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
    }
}