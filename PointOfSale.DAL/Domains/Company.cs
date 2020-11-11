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
        public string Name { get; set; }

        [StringLength(5)]
        [Required]
        public string PrefixName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string NaturalOfWork { get; set; }

        [MaxLength(1000)]
        [Required]

        public string Address { get; set; }

        [MaxLength(50)]
        [Required]

        public string Phone { get; set; }

        public string CountOfEmployees { get; set; }

        public int CountOfBranches { get; set; }


        [MaxLength(100)]
        [Required]
        public string OwnerName { get; set; }

        [MaxLength(50)]
        [Required]
        public string OwnerPhone { get; set; }

        public string EmailAddress { get; set; }
        public string Website { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
        public bool isActive { get; set; }

        public virtual List<Department> Departments { get; set; }

        public virtual List<SahlUserIdentity>  SahlUsers { get; set; }

        public virtual List<SahlApplicationsCompanies> SahlApplicationsCompanies { get; set; }


        public virtual List<Branch>  Branches { get; set; }


        public virtual List<Product> Products { get; set; }
    }
}
