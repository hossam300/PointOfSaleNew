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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string UserImage { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
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
