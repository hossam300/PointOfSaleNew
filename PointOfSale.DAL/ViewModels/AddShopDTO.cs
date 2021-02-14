using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class AddShopDTO
    {
        public int Id { get; set; }
        //[Required(ErrorMessageResourceName = "ShopRequired", ErrorMessageResourceType = typeof(Resources.Resources))]
      //  [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "Branch", ResourceType = typeof(Resources.Resources))]
        public int? BranchId { get; set; }
        [Display(Name = "IsRestaurant", ResourceType = typeof(Resources.Resources))]
        public bool IsRestaurant { get; set; }
        [Display(Name = "LoginWithEmployees", ResourceType = typeof(Resources.Resources))]
        public bool LoginWithEmployees { get; set; }
        [Display(Name = "AllowedEmployees", ResourceType = typeof(Resources.Resources))]
        public virtual List<ShopEmployeeDTO> AllowedEmployees { get; set; }
    }
}
