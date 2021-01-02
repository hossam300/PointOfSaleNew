using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    [Table("Users")]
    public class AspNetUser : IdentityUser
    {
        public AspNetUser()
        {
            UserRoles = new List<ApplicationUserRole>();
        }
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resources))]
        public string FirstName { get; set; }
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resources))]
        public string LastName { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Resources.Resources))]
        public string Phone { get; set; }
        [Display(Name = "UserImage", ResourceType = typeof(Resources.Resources))]
        public string UserImage { get; set; }
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Resources))]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resources))]
        public bool IsActive { get; set; } = true;
        public virtual List<ApplicationUserRole> UserRoles { get; set; }
        
    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual AspNetUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
    public class ApplicationRole : IdentityRole
    {
        public virtual List<ApplicationUserRole> UserRoles { get; set; }
    }
}
