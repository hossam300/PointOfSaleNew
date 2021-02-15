using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ProductPurchaseDTO
    {
        public int Id { get; set; }

        [Display(Name = "Procurement", ResourceType = typeof(Resources.Resources))]
        public Procurement Procurement { get; set; }
        public virtual List<VendorTaxDTO> VendorTaxes { get; set; }
        [Display(Name = "ControlPolicy", ResourceType = typeof(Resources.Resources))]
        public ControlPolicy ControlPolicy { get; set; }
        [Display(Name = "PurchaseDescription", ResourceType = typeof(Resources.Resources))]
        public string PurchaseDescription { get; set; }
    }
}
