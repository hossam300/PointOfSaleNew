using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace PointOfSale.DAL.ViewModels
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
            CustomerContacts = new List<CustomerContactDTO>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "CustomerName")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Customer Type")]
        public  CustomerType CustomerType { get; set; }
        [Display(Name = "Customer Image")]
        public string CustomerImage { get; set; }
        [Required(ErrorMessage = "Address Type Requird")]
        [Display(Name = "Address Type")]
        public int AddressTypeId { get; set; }
        public virtual AddressTypeDTO AddressType { get; set; }
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
        public string JobPointOfSaleition { get; set; }
        [Display(Name = "Website Link")]
        public string WebsiteLink { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        public virtual List<CustomerContactDTO> CustomerContacts { get; set; }
    }
}
