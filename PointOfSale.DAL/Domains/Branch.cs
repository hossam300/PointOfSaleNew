using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace  PointOfSale.DAL.Domains
{
    public class Branch
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [StringLength(500)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1000)]
        [Required]

        public string Address { get; set; }

        [MaxLength(50)]
        [Required]

        public string Phone { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public bool isActive { get; set; }

        [Required]
        public bool isDeleted { get; set; }

        [Required]
        public string CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public string DeletedUserId { get; set; }
        
        public DateTime DeleteDate { get; set; }



    }
}
