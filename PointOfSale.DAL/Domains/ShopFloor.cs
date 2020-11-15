namespace PointOfSale.DAL.Domains
{
    public class ShopFloor
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}