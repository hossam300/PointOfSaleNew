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
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Customer Type")]
        public virtual CustomerType CustomerType { get; set; }
        [Display(Name = "Customer Image")]
        public string CustomerImage { get; set; }
        [Required( ErrorMessage ="Address Type Requird")]
        [Display(Name = "Address Type")]
        public int AddressTypeId { get; set; }
        public virtual AddressType AddressType { get; set; }
        [Display(Name = "Phone")]
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
        [Display(Name = "TaxID")]
        public string TaxID { get; set; }
        [Display(Name = "Job PointOfSaleition")]
        public string JobPosition { get; set; }
        [Display(Name = "Website Link")]
        public string WebsiteLink { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        public string CreatorId { get; set; } 
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public virtual List<CustomerContact> CustomerContacts { get; set; }
    }
    public enum CustomerType
    {
        Individual = 1,
        Company = 2
    }
}
