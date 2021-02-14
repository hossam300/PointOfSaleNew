using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class DropDownList
    {
        public int Id { get; set; }
        [Display(Name = "DropDownListName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
    }
}
