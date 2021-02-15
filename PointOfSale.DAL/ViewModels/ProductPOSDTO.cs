using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ProductPOSDTO
    {
        public int Id { get; set; }
        [Display(Name = "AvailableInPOS", ResourceType = typeof(Resources.Resources))]
        public bool AvailableInPOS { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Resources.Resources))]
        public int? CategoryId { get; set; }
        public virtual ProductCategoryDTO Category { get; set; }
        [Display(Name = "ToWeighWithScale", ResourceType = typeof(Resources.Resources))]
        public bool ToWeighWithScale { get; set; }
    }
}
