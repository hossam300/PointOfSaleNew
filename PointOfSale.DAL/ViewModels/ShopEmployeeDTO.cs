using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopEmployeeDTO
    {
        public int Id { get; set; }
        [Display(Name = "User", ResourceType = typeof(Resources.Resources))]
        public string UserId { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
    }
}
