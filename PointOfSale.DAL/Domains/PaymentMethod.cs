using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        [Display(Name = "MehtodName", ResourceType = typeof(Resources.Resources))]
        public string MehtodName { get; set; }
        [Display(Name = "Cash", ResourceType = typeof(Resources.Resources))]
        public bool Cash { get; set; }
        //Intermediary Account
    }
}