using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class ShopEmployee
    {
        public int Id { get; set; }
        [Display(Name = "User", ResourceType = typeof(Resources.Resources))]
        public string UserId { get; set; }
        public virtual SahlUserIdentity User { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}