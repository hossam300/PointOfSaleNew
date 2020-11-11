using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class SahlApplicationsCompanies
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ApplicationId")]
        public int ApplicationId { get; set; }
        public virtual SahlApplication Application { get; set; }

        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [NotMapped]
        public bool isTrail { get; set; }

        [Required]
        [MaxLength(1000)]
        public string StartDate { get; set; }// ComPointOfSaleit: Encrypted Text of (PrefixName+RegistrationDate(ddMMYYY)+StartDate(ddMMYYYY))

        [Required]
        [MaxLength(1000)]
        public string ExpireDate { get; set; } // ComPointOfSaleit: Encrypted Text of (PrefixName+RegistrationDate(ddMMYYY)+ExprireDate(ddMMYYYY))
    }
}
