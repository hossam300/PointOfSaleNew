using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class CustomerContact
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
        public string ContactName { get; set; }
        public string ContactImage { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Company Address Street 1")]
        public string CompanyAddressStreet1 { get; set; }
        [Display(Name = "Company Address Street 2")]
        public string CompanyAddressStreet2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
    }
}