using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class Floor
    {
        public Floor()
        {
            Tables = new List<FloorTable>();
            ShopFloors = new List<ShopFloor>();
        }
        public int Id { get; set; }

        [Display(Name = "FloorName", ResourceType = typeof(Resources.Resources))]
        public string FloorName { get; set; }
        public virtual List<FloorTable> Tables { get; set; }
        public virtual List<ShopFloor> ShopFloors { get; set; }

    }
}