using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class ShopFloor
    {
        public int Id { get; set; }
        [Display(Name = "Floor", ResourceType = typeof(Resources.Resources))]
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}