using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class Supplier
    {
        public Supplier()
        {
            SupplierContacts = new List<SupplierContact>();
            PurchaseOrders = new List<PurchaseOrder>();
        }
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "SupplierRequired", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = "CustomerTypeRequired", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "SupplierType", ResourceType = typeof(Resources.Resources))]
        public virtual CustomerType SupplierType { get; set; }
        [Display(Name = "SupplierImage", ResourceType = typeof(Resources.Resources))]
        public string SupplierImage { get; set; }
        [Required(ErrorMessageResourceName = "AddressTypeRequired", ErrorMessageResourceType = typeof(Resources.Resources))]
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
        public virtual List<SupplierContact> SupplierContacts { get; set; }
        public virtual List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}