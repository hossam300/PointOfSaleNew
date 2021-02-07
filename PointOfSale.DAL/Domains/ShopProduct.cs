using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class ShopProduct
    {
        public int Id { get; set; }
        [Display(Name = "Product", ResourceType = typeof(Resources.Resources))]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        [Display(Name = "CurrentQuantity", ResourceType = typeof(Resources.Resources))]
        public int CurrentQuantity { get; set; }
        [Display(Name = "OldQuantity", ResourceType = typeof(Resources.Resources))]
        public int OldQuantity { get; set; }
        [Display(Name = "ActualQuantity", ResourceType = typeof(Resources.Resources))]
        public int ActualQuantity { get; set; }
        [Display(Name = "CreatedUserId", ResourceType = typeof(Resources.Resources))]
        public string CreatorId { get; set; }
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Resources))]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Display(Name = "ModfiedUserId", ResourceType = typeof(Resources.Resources))]
        public string ModfiedUserId { get; set; }
        [Display(Name = "ModfiedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime ModfiedDate { get; set; } 
    }
}
