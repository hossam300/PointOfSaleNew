using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class SahlUserIdentity : IdentityUser
    {

        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }

        public string Name_AR { get; set; }

        public string ImagePath { get; set; }
        public bool isActive { get; set; }





        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}
