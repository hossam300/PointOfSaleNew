using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ProductInventoryDTO
    {
        public int Id { get; set; }
        [Display(Name = "Manufacture", ResourceType = typeof(Resources.Resources))]
        public bool Manufacture { get; set; }
        [Display(Name = "Buy", ResourceType = typeof(Resources.Resources))]
        public bool Buy { get; set; }
        [Display(Name = "Weight", ResourceType = typeof(Resources.Resources))]
        public double Weight { get; set; } = 0;
        [Display(Name = "Volume", ResourceType = typeof(Resources.Resources))]
        public double Volume { get; set; } = 0;
        [Display(Name = "ManufacturingLeadTime", ResourceType = typeof(Resources.Resources))]
        public double ManufacturingLeadTime { get; set; } = 0;
        [Display(Name = "CustomerLeadTime", ResourceType = typeof(Resources.Resources))]
        public double CustomerLeadTime { get; set; } = 0;
        [Display(Name = "HSCode", ResourceType = typeof(Resources.Resources))]
        public string HSCode { get; set; }
        [Display(Name = "DescriptionForDeliveryOrders", ResourceType = typeof(Resources.Resources))]
        public string DescriptionForDeliveryOrders { get; set; }
        [Display(Name = "DescriptionForReceipts", ResourceType = typeof(Resources.Resources))]
        public string DescriptionForReceipts { get; set; }
    }
}
