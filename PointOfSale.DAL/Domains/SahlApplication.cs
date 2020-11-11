using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    [Table("SahlApplication")]
    public class SahlApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(500)]
        [Required]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [NotMapped]
        public bool isSelected { get; set; }

        [Required]
        public bool isActive { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public string APPNAME { get; set; }

        public virtual ICollection<SahlApplicationsCompanies> SahlApplicationsCompanies { get; set; }
    }
}
