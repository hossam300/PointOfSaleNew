using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace  PointOfSale.DAL.Domains
{
     
    public class Company
    { //test test
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(500)]
        [Required]
        [Display(Name = "CompanyName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }

        [StringLength(5)]
        [Required]
        [Display(Name = "PrefixName", ResourceType = typeof(Resources.Resources))]
        public string PrefixName { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "NaturalOfWork", ResourceType = typeof(Resources.Resources))]
        public string NaturalOfWork { get; set; }

        [MaxLength(1000)]
        [Required]
        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
        public string Address { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Phone", ResourceType = typeof(Resources.Resources))]
        public string Phone { get; set; }
        [Display(Name = "CountOfEmployees", ResourceType = typeof(Resources.Resources))]
        public string CountOfEmployees { get; set; }
        [Display(Name = "CountOfBranches", ResourceType = typeof(Resources.Resources))]
        public int CountOfBranches { get; set; }


        [MaxLength(100)]
        [Required]
        [Display(Name = "OwnerName", ResourceType = typeof(Resources.Resources))]
        public string OwnerName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "OwnerPhone", ResourceType = typeof(Resources.Resources))]
        public string OwnerPhone { get; set; }
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resources))]
        public string EmailAddress { get; set; }
        [Display(Name = "Website", ResourceType = typeof(Resources.Resources))]
        public string Website { get; set; }
        [Display(Name = "RegistrationDate", ResourceType = typeof(Resources.Resources))]
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resources))]
        public bool isActive { get; set; }

        public virtual List<Department> Departments { get; set; }

        public virtual List<SahlUserIdentity>  SahlUsers { get; set; }

        public virtual List<SahlApplicationsCompanies> SahlApplicationsCompanies { get; set; }


        public virtual List<Branch>  Branches { get; set; }


        public virtual List<Product> Products { get; set; }
    }
}
