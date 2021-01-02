using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class Customer
    {
        public Customer()
        {
            CustomerContacts = new List<CustomerContact>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Required]
        [Display(Name = "CustomerType", ResourceType = typeof(Resources.Resources))]
        public virtual CustomerType CustomerType { get; set; }
        [Display(Name = "CustomerImage", ResourceType = typeof(Resources.Resources))]
        public string CustomerImage { get; set; }
        [Required( ErrorMessage ="Address Type Requird")]
        [Display(Name = "AddressType", ResourceType = typeof(Resources.Resources))]
        public int AddressTypeId { get; set; }
        public virtual AddressType AddressType { get; set; }
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
        [Display(Name = "TaxID", ResourceType = typeof(Resources.Resources))]
        public string TaxID { get; set; }
        [Display(Name = "JobPosition", ResourceType = typeof(Resources.Resources))]
        public string JobPosition { get; set; }
        [Display(Name = "Website", ResourceType = typeof(Resources.Resources))]
        public string WebsiteLink { get; set; }
        [Display(Name = "Title", ResourceType = typeof(Resources.Resources))]
        public string Title { get; set; }
        [Display(Name = "Tags", ResourceType = typeof(Resources.Resources))]
        public string Tags { get; set; }
        [Display(Name = "CreatedUserId", ResourceType = typeof(Resources.Resources))]
        public string CreatorId { get; set; }
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Resources))]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public virtual List<CustomerContact> CustomerContacts { get; set; }
    }
    public enum CustomerType
    {
        [Display(Name = "Individual", ResourceType = typeof(Resources.Resources))]
        Individual = 1,
        [Display(Name = "Company", ResourceType = typeof(Resources.Resources))]
        Company = 2
    }
}
