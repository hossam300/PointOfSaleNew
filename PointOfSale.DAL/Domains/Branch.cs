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
        [Display(Name = "Company", ResourceType = typeof(Resources.Resources))]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [StringLength(500)]
        [Required]
        [Display(Name = "BranchName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }

        [MaxLength(1000)]
        [Required]
        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
        public string Address { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Phone", ResourceType = typeof(Resources.Resources))]
        public string Phone { get; set; }

        [EmailAddress]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resources))]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resources))]
        public bool isActive { get; set; }

        [Required]
        [Display(Name = "isDeleted", ResourceType = typeof(Resources.Resources))]
        public bool isDeleted { get; set; }

        [Required]
        [Display(Name = "CreatedUserId", ResourceType = typeof(Resources.Resources))]
        public string CreatedUserId { get; set; }

        [Required]
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Resources))]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "DeletedUserId", ResourceType = typeof(Resources.Resources))]
        public string DeletedUserId { get; set; }
        [Display(Name = "DeleteDate", ResourceType = typeof(Resources.Resources))]
        public DateTime DeleteDate { get; set; }



    }
}
