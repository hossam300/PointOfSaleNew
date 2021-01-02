using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class FloorTable
    {
        public int Id { get; set; }
        [Display(Name = "TableName", ResourceType = typeof(Resources.Resources))]
        public string TableName { get; set; }
        [Display(Name = "Seats", ResourceType = typeof(Resources.Resources))]
        public int Seats { get; set; }
        [Display(Name = "Floor", ResourceType = typeof(Resources.Resources))]
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }
    }
}