using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
   public class CustomerContactDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public  CustomerDTO Customer { get; set; }
        public int ContactTypeId { get; set; }
        public ContactTypeDTO ContactType { get; set; }
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
