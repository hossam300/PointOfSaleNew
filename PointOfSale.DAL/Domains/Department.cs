using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace  PointOfSale.DAL.Domains
{
    public class Department
    {
        public Department()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(500)]
        [Required]
        [Display(Name = "DepartmentName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "Company", ResourceType = typeof(Resources.Resources))]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resources))]
        public bool isActive { get; set; }
    }
}
