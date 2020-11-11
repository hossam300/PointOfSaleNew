using System.Collections.Generic;

namespace PointOfSale.DAL.Domains
{
    public class Floor
    {
        public Floor()
        {
            Tables = new List<FloorTable>();
        }
        public int Id { get; set; }
        public string FloorName { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual List<FloorTable> Tables { get; set; }

    }
}