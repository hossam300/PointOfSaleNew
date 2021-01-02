using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class Tax
    {
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
    }
}