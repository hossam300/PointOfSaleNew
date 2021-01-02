using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class CustomerContact
    {
        public int Id { get; set; }
        [Display(Name = "Customer", ResourceType = typeof(Resources.Resources))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [Display(Name = "ContactType", ResourceType = typeof(Resources.Resources))]
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
        [Display(Name = "ContactName", ResourceType = typeof(Resources.Resources))]
        public string ContactName { get; set; }
        [Display(Name = "ContactImage", ResourceType = typeof(Resources.Resources))]
        public string ContactImage { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Resources.Resources))]
        public string Phone { get; set; }
        [Display(Name = "Mobile", ResourceType = typeof(Resources.Resources))]
        public string Mobile { get; set; }
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }
        [Display(Name = "CompanyAddressStreet1", ResourceType = typeof(Resources.Resources))]
        public string CompanyAddressStreet1 { get; set; }
        [Display(Name = "CompanyAddressStreet2", ResourceType = typeof(Resources.Resources))]
        public string CompanyAddressStreet2 { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resources.Resources))]
        public string City { get; set; }
        [Display(Name = "Country", ResourceType = typeof(Resources.Resources))]
        public string Country { get; set; }
    }
}