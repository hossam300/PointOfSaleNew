namespace PointOfSale.DAL.Domains
{
    public class FloorTable
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int Seats { get; set; }
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }
    }
}